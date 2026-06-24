using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using MineralCollection.Domain; 

namespace MineralCollection.Frontend.ViewModels
{
    public class HomeViewModel : IDisposable
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private string _searchTerm = "";

        public event Action? OnChange;
        public List<Mineral>? Minerals { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool IsSaving { get; set; }
        public string ActiveTab { get; set; } = "explorer";
        public bool ShowCoordPicker { get; set; }
        public Mineral? ActiveMineral { get; set; }
        public double? PendingBreitengrad { get; set; }
        public double? PendingLaengengrad { get; set; }
        public string? SelectedImageUrl { get; set; }
        public bool ShowImagePreview { get; set; }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    if (ActiveTab == "explorer")
                    {
                        _ = UpdateMapAsync();
                    }
                }
            }
        }

        public HomeViewModel(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public IEnumerable<Mineral> FilteredMinerals => Minerals?
            .Where(m => string.IsNullOrWhiteSpace(SearchTerm) ||
                        m.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                        (m.Region != null && m.Region.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (m.Fundort != null && m.Fundort.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (m.Land != null && m.Land.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)))
            .OrderByDescending(m => m.Id == 0)
            .ThenBy(m =>
            {
                if (SortBy == "Name") return (object)(m.Name ?? "");
                if (SortBy == "Region") return (object)(m.Region ?? "");
                if (int.TryParse(m.Nummer, out int n)) return (object)n;
                return (object)(m.Nummer ?? "");
            })
            ?? Enumerable.Empty<Mineral>();

        public async Task LoadMineralsAsync()
        {
            Minerals = await _http.GetFromJsonAsync<List<Mineral>>("api/minerals");
            NotifyStateChanged();
        }

        public async Task HandleTabChangedAsync(string newTab)
        {
            ActiveTab = newTab;
            if (ActiveTab == "explorer")
            {
                await Task.Delay(200);
                await _js.InvokeVoidAsync("mapBox.initialize", 51.1657, 10.4515, 5);
                if (Minerals != null)
                {
                    await _js.InvokeVoidAsync("mapBox.addMarkers", Minerals);
                }
            }
        }

        public async Task UpdateMapAsync() => 
            await _js.InvokeVoidAsync("mapBox.addMarkers", FilteredMinerals.ToList());

        public async Task HandleSelectAsync(int id) => 
            await _js.InvokeVoidAsync("mapBox.focusMineral", id);

        public void CreateNewMineral()
        {
            SearchTerm = "";
            if (Minerals == null) return;

            var newMineral = new Mineral
            {
                Name = "Neues Mineral",
                Nummer = "0",
                Images = new List<MineralImage>() 
            };

            Minerals.Insert(0, newMineral);
            NotifyStateChanged();
        }

        public async Task SaveChangesAsync()
        {
            if (Minerals == null || IsSaving) return;
            IsSaving = true;
            NotifyStateChanged();

            try
            {
                foreach (var m in FilteredMinerals.ToList())
                {
                    HttpResponseMessage response;

                    if (m.Id == 0)
                    {
                        response = await _http.PostAsJsonAsync("api/minerals", m);
                        if (response.IsSuccessStatusCode)
                        {
                            var createdMineral = await response.Content.ReadFromJsonAsync<Mineral>();
                            if (createdMineral != null) m.Id = createdMineral.Id;
                        }
                    }
                    else
                    {
                        response = await _http.PutAsJsonAsync($"api/minerals/{m.Id}", m);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Fehler bei ID {m.Id}: {response.ReasonPhrase}");
                    }
                }
                await _js.InvokeVoidAsync("alert", "Änderungen gespeichert!");
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", "Fehler: " + ex.Message);
            }
            finally
            {
                IsSaving = false;
                NotifyStateChanged();
            }
        }

        public async Task OpenCoordPickerAsync(Mineral m)
        {
            ActiveMineral = m;
            PendingBreitengrad = m.Breitengrad;
            PendingLaengengrad = m.Laengengrad;
            ShowCoordPicker = true;
            NotifyStateChanged();

            await Task.Delay(150);
            
            // Reicht sich selbst als JSInvokable-Referenz an JavaScript weiter!
            var dotNetObjRef = DotNetObjectReference.Create(this);
            
            string startLat = PendingBreitengrad?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "51.16";
            string startLon = PendingLaengengrad?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "10.45";

            await _js.InvokeVoidAsync("mapBox.initPicker", dotNetObjRef, startLat, startLon);
        }

        [JSInvokable]
        public void UpdateActiveCoordinates(string lat, string lon)
        {
            if (double.TryParse(lat, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var dLat))
                PendingBreitengrad = dLat;

            if (double.TryParse(lon, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var dLon))
                PendingLaengengrad = dLon;

            NotifyStateChanged();
        }

        public void ApplySelectedCoordinates()
        {
            if (ActiveMineral != null)
            {
                ActiveMineral.Breitengrad = PendingBreitengrad;
                ActiveMineral.Laengengrad = PendingLaengengrad;
            }

            CloseCoordPicker();
        }

        public void CloseCoordPicker()
        {
            ShowCoordPicker = false;
            ActiveMineral = null;
            PendingBreitengrad = null;
            PendingLaengengrad = null;
            NotifyStateChanged();
        }

        public void OpenImagePreview(string fileName)
        {
            SelectedImageUrl = fileName;
            ShowImagePreview = true;
        }

        public void CloseImagePreview()
        {
            ShowImagePreview = false;
            SelectedImageUrl = null;
        }

        public async Task HandleImageUploadAsync(IBrowserFile file, Mineral m)
        {
            if (file == null) return;

            try
            {
                using var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10));
                content.Add(fileContent, "file", file.Name);

                var response = await _http.PostAsync("api/image/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                    if (result != null && !string.IsNullOrEmpty(result.fileName))
                    {
                        if (m.Images == null) m.Images = new List<MineralImage>();
                        
                        m.Images.Clear();
                        m.Images.Add(new MineralImage { FileName = result.fileName });
                        
                        NotifyStateChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                await _js.InvokeVoidAsync("alert", "Upload-Fehler: " + ex.Message);
            }
        }

        public async Task DeleteMineralAsync(Mineral m)
        {
            bool confirmed = await _js.InvokeAsync<bool>("confirm", $"Möchtest du '{m.Name}' wirklich löschen?");
            if (!confirmed) return;

            var fileName = m.Images?.FirstOrDefault()?.FileName;

            if (m.Id != 0)
            {
                await _http.DeleteAsync($"api/minerals/{m.Id}");
            }
            else if (!string.IsNullOrEmpty(fileName))
            {
                await _http.DeleteAsync($"api/image/{fileName}");
            }

            Minerals?.Remove(m);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void Dispose() => OnChange = null;

        public class UploadResult
        {
            public string? fileName { get; set; }
        }
    }
}
