using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Threading.Tasks;
using XamFamDiary.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamFamDiary.ViewModels
{
    public class DiaryViewModel : BaseViewModel
    {
        private readonly IHttpService _httpService;

        public Diary UserDiary { get; set; }
        public string Location { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Areas { get; set; }
        public List<string> Events { get; set; }
        public List<string> Categories { get; set; }
        public ObservableCollection<string> Photos { get; set; }
        
        public DiaryViewModel(IHttpService httpService)
        {
            _httpService = httpService;

            //Add Dates
            var date = DateTime.Now;
            Dates = new List<string>()
            {
                date.ToString("MM/dd/yyyy"),
                date.AddDays(1).ToString("MM/dd/yyyy"),
                date.AddDays(2).ToString("MM/dd/yyyy"),
                date.AddDays(3).ToString("MM/dd/yyyy"),
                date.AddDays(4).ToString("MM/dd/yyyy"),
                date.AddDays(5).ToString("MM/dd/yyyy")
            };

            //Add Areas
            Areas = new List<string>()
            {
                "Area 51",
                "Area 52",
                "Area 53",
                "Area 54",
                "Area 55"
            };

            //Add Categories
            Categories = new List<string>()
            {
                "Category A",
                "Category B",
                "Category C",
                "Category D",
                "Category E"
            };

            //Add Events
            Events = new List<string>()
            {
                "Event A",
                "Event B",
                "Event C",
                "Event D",
                "Event E"
            };
            UserDiary = new Diary()
            {
                Tag = "Tags",
                LinkEvent = true,
                IncludePhoto = true,
                Date = "Select Date",
                Area = "Select  Area",
                Event = "Select an event",
                Category = "Select Category"
            };
            Photos = new ObservableCollection<string>();
        }

        /// <summary>
        /// Set view bindings asynchronsly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task InitializeAsync(object data)
        {
            await base.InitializeAsync(data);

            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                //Prevent memory leaks with string builder vs. string interpolation
                var locationString = new StringBuilder();
                locationString.Append(location.Latitude);
                locationString.Append(" | ");
                locationString.Append(location.Longitude);

                Location = locationString.ToString();
            }
        }

        /// <summary>
        /// Selects photo from device
        /// </summary>
        public ICommand AddPhotoCommand => new Command(async () =>
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions());

                if (result != null)
                    Photos.Add(result.FullPath);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        });

        /// <summary>
        /// Removes photo from list
        /// </summary>
        public ICommand RemovePhotoCommand => new Command<string>(async (path) =>
        {
            Photos.Remove(path);
        });

        /// <summary>
        /// Post diary data to api
        /// </summary>
        public ICommand SendDairyCommand => new Command(async () =>
        {
            UserDiary.Photos = Photos;
            var response = await _httpService.PostAsync<Diary>(
                "https://reqres.in/api/users",
                UserDiary
                );
        });
    }
}