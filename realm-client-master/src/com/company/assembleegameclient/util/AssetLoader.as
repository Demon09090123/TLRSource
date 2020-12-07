package com.company.assembleegameclient.util
{
   import com.company.assembleegameclient.engine3d.Model3D;
   import com.company.assembleegameclient.map.GroundLibrary;
   import com.company.assembleegameclient.map.RegionLibrary;
   import com.company.assembleegameclient.objects.ObjectLibrary;
import com.company.assembleegameclient.objects.particles.ParticleLibrary;
   import com.company.assembleegameclient.parameters.Parameters;
   import com.company.assembleegameclient.sound.SoundEffectLibrary;
import com.company.assembleegameclient.ui.options.Options;
import com.company.util.AssetLibrary;
   import flash.utils.ByteArray;
import flash.utils.Timer;

import kabam.rotmg.assets.EmbeddedAssets;
   import kabam.rotmg.assets.EmbeddedData;

import org.osflash.signals.Signal;

public class AssetLoader
   {
      public function AssetLoader()
      {
          super();
      }
      
      public function load() : void
      {
          for (var dKey:String in EmbeddedAssets.assetsDict) {
              var asset:Object = EmbeddedAssets.assetsDict[dKey];
              AssetLibrary.addImageSet(dKey, asset["bitmapData"], asset["cw"], asset["ch"]);
          }
          //ANIMATED IMAGES
          for (var aKey:String in EmbeddedAssets.animatedAssetsDict) {
              var aAsset:Object = EmbeddedAssets.animatedAssetsDict[aKey];

              AnimatedChars.add(aKey, aAsset["bitmapData"], aAsset["mask"], aAsset["cw"], aAsset["ch"], aAsset["w"], aAsset["h"], aAsset["dir"]);
          }
          //SOUND
          for each(var sKey:String in EmbeddedAssets.soundEffectDict) {
              SoundEffectLibrary.load(sKey);
          }

          for(var name:String in EmbeddedAssets.models_)
          {
              var barr:ByteArray = EmbeddedAssets.models_[name];
              var model:String = barr.readUTFBytes(barr.length);
              Model3D.parse3DOBJ(name, barr);
              Model3D.parseFromOBJ(name, model);
          }

          ParticleLibrary.parseFromXML(XML(new EmbeddedAssets.particlesEmbed()));

          //GROUND FILE
          for each(var groundObj:* in EmbeddedData.groundFiles) {
              GroundLibrary.parseFromXML(XML(groundObj))
          }

          //OBJECTFILE
          for each(var objectObj:* in EmbeddedData.objectFiles)
          {
              ObjectLibrary.parseFromXML(XML(objectObj));
          }
          //REGION FILE
          for each(var regionXML:* in EmbeddedData.regionFiles)
          {
              RegionLibrary.parseFromXML(XML(regionXML));
          }

          Parameters.load();//IMAGES
          Options.refreshCursor();
      }
   }
}
