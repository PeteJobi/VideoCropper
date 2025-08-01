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
using VideoCropper;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cropper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string ffmpegLocation;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                GoToCropper(items[0].Path);
            }
        }

        private void MainPage_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void ShowFilePicker(object sender, RoutedEventArgs e)
        {
            string[] allSupportedTypes = [".mkv", ".mp4"];
            var filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            foreach (var supportedType in allSupportedTypes)
            {
                filePicker.FileTypeFilter.Add(supportedType);
            }
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow.Window);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            var file = await filePicker.PickSingleFileAsync();
            GoToCropper(file.Path);
        }

        private async void GoToCropper(string videoPath)
        {
            var ffmpegPath = string.Empty;
            try
            {
                ffmpegPath = Path.Join(Package.Current.InstalledLocation.Path, "Assets/ffmpeg.exe");
            }
            catch (InvalidOperationException)
            {
                ffmpegPath = "Assets/ffmpeg.exe";
            }
            if (!File.Exists(ffmpegPath))
            {
                await ErrorDialog.ShowAsync();
                return;
            }
            Frame.Navigate(typeof(VideoCropperPage), new CropperProps { FfmpegPath = ffmpegPath, VideoPath = videoPath });
        }
    }
}
