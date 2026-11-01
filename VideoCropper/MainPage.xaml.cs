using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace VideoCropper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TitleBar _titleBar;
        private string? _ffmpegPath;
        private readonly EventHandler<string> _fileDropped;
        private readonly EventHandler<string> _ffmpegSet;
        public readonly string[] supportedTypes = [".mkv", ".mp4", ".mov", ".avi"];

        public MainPage()
        {
            _fileDropped = (_, path) => GoToCropper(path);
            _ffmpegSet = (_, path) => _ffmpegPath = path;
            InitializeComponent();
        }

        private void GoToCropper(string videoPath)
        {
            _ffmpegPath = videoPath;
            Frame.Navigate(typeof(VideoCropperPage), new CropperProps { FfmpegPath = _ffmpegPath, VideoPath = videoPath, TypeToNavigateTo = typeof(MainPage).FullName});
            _titleBar.IsBackButtonVisible = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is TitleBar titleBar)
            {
                _titleBar = titleBar;
                return;
            }
            _titleBar.IsBackButtonVisible = false;

            if (e.Parameter is string outputFile)
            {
                Console.WriteLine($"Path of output file is {outputFile}");
            }
        }
    }
}
