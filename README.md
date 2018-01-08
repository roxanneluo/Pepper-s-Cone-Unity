# Pepper's Cone: An Inexpensive Do-It-Yourself 3D Display
![![Tiger with Pepper's Cone](https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/PeppersConeSmall.gif)](https://github.com/roxanneluo/Pepper-s-Cone-Unity/raw/master/PeppersCone.gif)

This repo contains code to display 3D objects given calibrated distortion map. More information see our [paper](http://roxanneluo.github.io/PeppersCone.html) and [video](https://youtu.be/W2P-suog684).

## Requirement
1. Build your cone following the [instructions](http://roxanneluo.github.io/PeppersCone.html)
2. Get a tablet with gyroscope. Here I included the distortion map to calibrate the
   distortion of a 45-degree cone you just built on a [ 12.9'' iPad Pro](https://www.apple.com/ipad-pro/). 
	While this distortion map "roughly" works for other setup. But for best accuracy,
	please follow our paper to do the distortion calibration.
3. Distortion calibration. (Skip this if you use my calibrated scenario.)
4. If you want to try the binocular version of Pepper's Cone, please prepare a
   pair of red-cyan anaglyph glasses like [this](https://www.amazon.com/50-Pairs-Glasses-Anaglyph-Cardboard/dp/B009TZRIGG/ref=sr_1_5?ie=UTF8&qid=1515391228&sr=8-5&keywords=anaglyph+glasses)
	or [this](https://www.amazon.com/BIAL-Red-blue-Glasses-Anaglyph-game-Extra/dp/B01ANJXCU2/ref=sr_1_1_sspa?ie=UTF8&qid=1515391228&sr=8-1-spons&keywords=anaglyph+glasses&psc=1).

## Demo
In the paper we described two Pepper's Cone setup:
- a glass-free setup where the scene is only calibrated towards the center of the two eyes, see `Assets/Scenes/MonoPeppersConeMini.unity`,
- and a binocular setup, where you need to wear you need to wear red-cyan anaglyph
glasses, see `Assets/Scenes/StereoPeppersConeMini.unity`. With this one you can
see true depth from the stereo cues.

Build and run the corresponding scene on the tablet, place the cone at the
center of the rings, put a nickel for stabilization during rotation, 
and enjoy your 3D "hologram" at home :) (For better stabilization, you can use a
[sucker pad](https://www.amazon.com/Whaline-Suction-Plastic-Sucker-without/dp/B071WFNKTB/ref=sr_1_1_sspa?ie=UTF8&qid=1515392318&sr=8-1-spons&keywords=sucker+pad+office&psc=1))

The scenes here are the minimal setup required to show Pepepr's Cone. To add
your own 3D models, add your models as child gameobjects of `ModelController`
like those cylinders there.
You can also add sound to boost realism.

### UI Description
There are two main scenes in either unity scene.
- calibration scene,
- model scene.

Press `CalibOrModel` to switch between calibration and the model scene.
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

