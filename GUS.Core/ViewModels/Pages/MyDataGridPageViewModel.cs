using Newtonsoft.Json;
using System.Windows.Input;

namespace GUS.Core
{
    public class MyDataGridPageViewModel
    {
        public ObservableRangeCollection<MyDataGridViewModel> MyDataGridViewModels { get; set; } 
            = new ObservableRangeCollection<MyDataGridViewModel>();

        public ICommand LoadDataCommand { get; set; }

        public MyDataGridPageViewModel()
        {
            LoadDataCommand = new RelayCommand(LoadData);
        }

        private async void LoadData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://api-dbw.stat.gov.pl/api/1.1.0/area/area-area");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<List<MyDataGridViewModel>>(content);

                if (data != null && data.Count > 0)
                {
                    if (MyDataGridViewModels.Count > 0)
                    {
                        MyDataGridViewModels.Clear();
                    }

                    MyDataGridViewModels.AddRange(data);
                }
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
