# About
The original auther from [YouTube](https://www.youtube.com/) named [Michael Hicks](https://www.youtube.com/channel/UCsiPXCHman-xRFQ3jwvFj1A), posted a series back in 2018 designing a Game Engine with the FNA-XNA library using [C#](https://docs.microsoft.com/en-us/dotnet/csharp/).

## Conclusion
This is that same GameEngine and editor which you would have ended up with at the end of [his series](https://www.youtube.com/watch?v=WQOebBVIB0I) done in [VisualBasic.Net](https://docs.microsoft.com/en-us/dotnet/visual-basic/), go give his series a watch.

## Things you will need
  - You will either need to install the old 
    - [XNA Game Studio](https://www.microsoft.com/en-ca/download/details.aspx?id=23714)
  - Or get
    - [FNA](https://github.com/FNA-XNA/FNA/) _You must compile this on your own there is no NuGet package_
  - Runtimes
    - you will find the runtimes for [FNA](https://github.com/FNA-XNA/FNA/) in their [wiki page](https://github.com/FNA-XNA/FNA/wiki/1:-Download-and-Update-FNA#2-download-native-libraries)

## Things I added to the current structure
  - Basic 1 Player controller support
    - [PS4 Controller](https://direct.playstation.com/en-us/accessories/accessory/dualshock4-wireless-controller-for-ps4-jet-black.3001538) _i would asume that the xbox controler controls for this would be the exact same_
      - Left Stick for (Left, Right, Up _jump_)
      - Right Trigger _shoot_
