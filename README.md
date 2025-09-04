# Video Cropper
This repo provides a Windows desktop application for cropping videos. Only supports Windows 10 and 11 (not tested on other versions of Windows). Powered by FFMPEG.

<img width="1791" height="1023" alt="Screenshot 2025-07-28 103546" src="https://github.com/user-attachments/assets/e40e106c-9399-4eb4-87e7-f93264d6ee8b" />

## How to build
You need to have at least .NET 9 runtime installed to build the software. Download the latest runtime [here](https://dotnet.microsoft.com/en-us/download). If you're not sure which one to download, try [.NET 9.0 Version 9.0.203](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-9.0.203-windows-x64-installer)

In the project folder, run the below
```
dotnet publish -c Release -p:Platform=x64 -p:WindowsAppSDKSelfContained=true -p:WindowsPackageType=None
```
When that completes, go to `\bin\Release\net<version>-windows\win-x64` and you'll find the **VideoCropper.exe**.

## Run without building
You can also just download the release builds if you don't wish to build manually. Unfortunately packages created in WinUI 3 have to be signed with a certificate, and certificates sourced from trusted companies cost hundreds of dollars. If you wish to install the package, you'll have to install a certificate signed by myself, as described [here](https://github.com/PeteJobi/VideoCropper/releases/tag/cert). You only need to do this once - future updates will not require different certificates.

## How to use
When you open the program, you will be prompted to upload a video. Once that is done, you will be taken to the crop interface page. Drag and resize the crop frame as required. By default, the aspect ratio is not locked and you can resize freely, but if you wish to, click the "**Aspect Ratio**" button to toggle between locked and unlocked. You can choose from a bunch of predefined aspect ratios or reset to the original. Center the frame within the video with the "**Center Frame**" button.

As you drag and resize, the coordinates of the frame will reflect in the number boxes labelled **X**, **Y**, **Width** and **Height**. If you want to be exact, you can manually change any of these values and the frame will update to reflect your input. There are simple video controls that can help you see how parts of the video look within the frame.

If you want to see more clearly what's in the crop frame, especially if you intend to crop a small portion, you can tick the **Zoom into frame** checkbox.

When you're done setting up the crop frame, click the "**Crop!**" button to start cropping. A progress bar showing the progress of the crop operation will show up. While the process is on-going, you can choose to pause or cancel it. Once the process is done, you can click the "**View**" button to view the output. 

The output file will be created in the same folder and it will have the same file name with "__CROPPED_" appended to it. Be warned that if such a file exists, it will be deleted.

Click the **Go back** button to return to the file selection page.
