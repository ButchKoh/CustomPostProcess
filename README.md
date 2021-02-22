# CustomPostProcess
作ったPostProcessをここに置きます。<br>
This repository contains custom post-effect package files I developed.
![Videotogif](https://user-images.githubusercontent.com/64464106/107236836-70b5b900-6a69-11eb-8859-6a035b3747eb.gif)

## 環境設定/Environment Settings
Unity 2019.4.9f1<br>
Visual Studio 2019

## パッケージ依存/Package Dependency
Post Processing 3.0.1

## 導入方法/How to Install
1. [Window]->[Package Manager]でウィンドウを開く。<br>
  Open the package manager window by clicking [Window]->[Package Manager].<br>
![スクリーンショット (1617)](https://user-images.githubusercontent.com/64464106/107238645-6eecf500-6a6b-11eb-89e4-ed7c654384ca.png)<br>
2. ウィンドウの左上部の[+]ボタンから[Add package from git URL...]を選択し、下記URLをコピペで入力して[Add]を押す。<br>
  Press the [+] button and select [Add package from git URL...], then fill in the following URL and press the [Add] button.<br>
  https://github.com/ButchKoh/CustomPostProcess.git<br>
![スクリーンショット (1618)](https://user-images.githubusercontent.com/64464106/107238654-72807c00-6a6b-11eb-8eb3-9b28a27b89fd.png)

## 使用例/Example Usage
Root/Runtime/Sceneフォルダに最初のgifで使用したシーンがあります。Root/Runtime/Profilesフォルダにgifで使用したPost-process Volume用Profileがあります。<br>
Root / Runtime / Scene folder contains the scene file used in the first gif image. 
Root / Runtime / Profiles folder contains the preset profiles for Post-process Volume used in the gif.

## エフェクトの種類/Effects
1. ノイズをかける<br>
2. 画像を粗くする<br>
3. 輪郭抽出1<br>
4. 輪郭抽出2<br>
5. HSVをいじる<br>
6. モノトーン+所定範囲の色相の色<br>
7. 所定の範囲の色を画像に置き換える<br>
8. 法線を取る<br>
9. 深度を取る<br>
![fig](https://user-images.githubusercontent.com/64464106/108764122-882a9100-7595-11eb-92f9-2ad597f5acf8.png)
