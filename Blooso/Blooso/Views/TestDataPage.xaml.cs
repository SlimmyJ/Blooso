using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blooso.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Blooso.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestDataPage : ContentPage
    {
        private TestData _testData;

        public TestDataPage()
        {
            InitializeComponent();
            _testData = new TestData();
            _testData.MakeTestData();
        }
    }
}