
# Pepper's Cone: An Inexpensive Do-It-Yourself 3D Display
<p align="center">
  <img src="https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/doc/PeppersCone.gif" alt="Tiger with Pepper's Cone"/>
</p>

This repo contains the [Unity](https://unity3d.com/) project to display 3D objects given calibrated distortion map. More information see our [paper](http://roxanneluo.github.io/PeppersCone.html) and [video](https://youtu.be/W2P-suog684).

## How to DIY Your Own Pepper's Cone
1. Build a cone following the [instructions](#build_cone) below.
2. Get a tablet with gyroscope. Here we use [ 12.9'' iPad Pro](https://www.apple.com/ipad-pro/).
3. Distortion calibration. The distortion map in the repo is calibrated when placing the 45-degree cone you just built on a [ 12.9'' iPad Pro](https://www.apple.com/ipad-pro/). 
	While this distortion map "roughly" works for other setup, for best accuracy,
	please follow our paper to do the distortion calibration.
	(Skip this if you use my calibrated scenario.)
4. If you want to try the binocular version of Pepper's Cone, please prepare a
   pair of red-cyan anaglyph glasses like [this](https://www.amazon.com/50-Pairs-Glasses-Anaglyph-Cardboard/dp/B009TZRIGG/ref=sr_1_5?ie=UTF8&qid=1515391228&sr=8-5&keywords=anaglyph+glasses)
	or [this](https://www.amazon.com/BIAL-Red-blue-Glasses-Anaglyph-game-Extra/dp/B01ANJXCU2/ref=sr_1_1_sspa?ie=UTF8&qid=1515391228&sr=8-1-spons&keywords=anaglyph+glasses&psc=1).
5. Download [Unity](https://unity3d.com/). Build and run the demo scenes in Unity following the [Demo](#demo) section.

The assembly process is also shown in the [video](https://youtu.be/W2P-suog684?t=21).

## <a name="build_cone"></a>Build a Cone
Here I'll show you how to build the cone itself. If you just want to run the demo app, jump to the [Demo](#demo) section.
1. Download the template of a [45-degree cone](https://drive.google.com/open?id=1oQdD7Qd_Vl1tgVuC9qUqwRtVh-BpPNKS). More cone templates are available [here](https://drive.google.com/drive/folders/11HggZe3xQmn-K04CsQn9UBVVDNrPS_ql?usp=sharing).
2. Buy [clear 0.01-inch polycarbonate sheets](https://www.tapplastics.com) or even thinner. Feel free to use other **transparent** **thin** plastic sheets. PETG and many other materials also work.
3. Cut the plastic sheet using the template. Cut through all red curves and cut half-way through for the blue ones. You can
	- print out the template as a poster since it's much larger than A4 paper, and cut your plastic sheet manually.
	- Or let a **laser cutter** cut it according to the template. Nowadays there are many makerspaces in the neighbourhood like [UW's Makerspace](https://comotion.uw.edu/what-we-do/makerspace/) and [Metrix Create Space](http://www.metrixcreatespace.com) where laser cutters are available and cheap to use. Note that although using a laser cutter is more convenient and accurate, some laser cutter doesn't support cutting sheets of certain plastic material. **Be careful!**
4. Tape the openning of the cone at the bottom and put a nickel for statblization during rotation. Alternatively, you can put a [sucker pad](https://www.amazon.com/Whaline-Suction-Plastic-Sucker-without/dp/B071WFNKTB/ref=sr_1_1_sspa?ie=UTF8&qid=1515392318&sr=8-1-spons&keywords=sucker+pad+office&psc=1) in the opening.
									
<p align="center">
  <img src="https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/doc/cone_template.png" alt="cone template"/>
  <img src="https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/doc/sucker_pad.jpg" alt="sucker pad"/>
</p>

## <a name="demo"> </a>Demo
In the paper we described two Pepper's Cone setups:
- a glass-free setup where distortion is only calibrated towards the center of the two eyes, see `Assets/Scenes/MonoPeppersConeMini.unity`,
- and a binocular setup where you need to wear you need to wear red-cyan anaglyph
glasses, see `Assets/Scenes/StereoPeppersConeMini.unity`. With this one you can
see true stereo depth.

To test it in the Unity Editor, open the scene, hit play and hold `ctrl` key while moving the mouse around.
You will see how the 3D model is pre-distorted when being rotated around as in this [video](https://youtu.be/bA3ubMQyraI).

Build and run the corresponding scene on the tablet, place the cone and the
nickel at the center of the rings, and enjoy your 3D "hologram" at home :)
(P.S. you need to use Mac to deploy onto an iOS device.)

The scenes here are the minimal setup required to show 3D objects in Pepepr's Cone. To add
your own 3D models, add your models as child gameobjects of `ModelController`
like those cylinders there.
You can also add sound to boost realism.

### UI Description
There are two main scenes in `MonoPeppersConeMini` and `StereoPeppersConeMini`:
- calibration scene,
- and model scene.

Press `CalibOrModel` to switch between the calibration and the model scene.
At the model scene, press `Next Model` to switch
between different 3D models.

#### Calibration Scene
- **Concentric rings**. You should put the cone at the
center. 
- **Cylinder**. Pepper's Cone works the best when you place your head at the calibrated
position while it still works when your head is roughly centered. You can use
the cyclinder in the scene to calibrate your head position. The cylinder will be
reflected by the cone surface and then by the tablet screen again. Close one eye
to see the reflection of the cylinder on the tablet screen. When the reflection you see with
either eye is symmetric about the original displayed cylinder image, your head
is centered and at the calibrated position.
- **reset**. When the rotation estimation is off or towards undesired
  orientation, press `reset`.

## Notes about GoogleVR SDK
I use GoogleVR SDK 1.10 to get low-drift low-latency rotation estimation. I
can't use the latest 1.110.0 version because for now they don't open API to enable only rotation
estimation without stereo rendering. Please let me know if you find that
new GoogleVR SDK supports that. 

Currently, all GoogleVR related files are in `Assets/GoogleVR` and
`Assets/Plugins`. Only these folders, the gameobject `RotationManager` and the script
`Assets/Scripts/RotationManager.cs` need to be changed for upgrading GVR.
   
## Citation
```
@inproceedings{luo2017pepper,
  title={Pepper's Cone: An Inexpensive Do-It-Yourself 3D Display},
  author={Luo, Xuan and Lawrence, Jason and Seitz, Steven M},
  booktitle={Proceedings of the 30th Annual ACM Symposium on User Interface Software and Technology},
  pages={623--633},
  year={2017},
  organization={ACM}
}
```

## Contact
If you have any questions, please email Xuan Luo at
<xuanluo@cs.washington.edu>.

## License
[BSD](https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/LICENSE)
