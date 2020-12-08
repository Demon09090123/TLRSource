package com.company.assembleegameclient.appengine
{
   import com.company.assembleegameclient.objects.ObjectLibrary;
   import com.company.assembleegameclient.parameters.Parameters;
   import com.company.assembleegameclient.util.AnimatedChar;
   import com.company.assembleegameclient.util.AnimatedChars;
   import com.company.assembleegameclient.util.MaskedImage;
   import com.company.assembleegameclient.util.TextureRedrawer;
import com.company.assembleegameclient.util.redrawers.GlowRedrawer;
import com.company.util.CachingColorTransformer;
   import flash.display.BitmapData;
   import flash.geom.ColorTransform;

import kabam.rotmg.messaging.impl.data.StatData;

public class SavedCharacter
   {
      private static const dimmedCT:ColorTransform = new ColorTransform(0, 0, 0, 0.5, 0, 0, 0, 0);
      private static const selectedCT:ColorTransform = new ColorTransform(0.75, 0.75, 0.75, 1, 0, 0, 0, 0);

      private var charXML_:XML;
      private var statsXML:XML;
      
      public function SavedCharacter(charXML:XML)
      {
         super();
         this.charXML_ = charXML;
      }

      public function getStats() : XML {
          return XML(this.charXML_.Stats);
      }
      public function objectType() : int {
         return int(this.charXML_.ObjectType);
      }
      public function skinType() : int {
         return int(this.charXML_.Texture);
      }
      public function level() : int {
         return int(this.charXML_.Level);
      }
      public function tex1() : int {
         return int(this.charXML_.Tex1);
      }
      public function tex2() : int
      {
         return int(this.charXML_.Tex2);
      }
      public function nextLvlXP():int {
          return int(this.charXML_.NextLevelEXP);
      }
      public function xp() : int
      {
         return int(this.charXML_.Exp);
      }
      public function toString() : String
      {
         return "SavedCharacter: {" + this.charXML_ + "}";
      }

       public static function getImage(savedChar:SavedCharacter, playerXML:XML, dir:int, action:int, p:Number, selected:Boolean, dimmed:Boolean) : BitmapData
       {
           var animatedChar:AnimatedChar = AnimatedChars.getAnimatedChar(String(playerXML.AnimatedTexture.File),int(playerXML.AnimatedTexture.Index));
           var image:MaskedImage = animatedChar.imageFromDir(dir,action,p);
           var tex1:int = savedChar != null?int(savedChar.tex1()):int(null);
           var tex2:int = savedChar != null?int(savedChar.tex2()):int(null);
           var bd:BitmapData = TextureRedrawer.resize(image.image_,image.mask_,100,false,tex1,tex2);
           if (dimmed)
           {
               bd = CachingColorTransformer.transformBitmapData(bd,dimmedCT);
           }
           else if(!selected)
           {
               bd = CachingColorTransformer.transformBitmapData(bd,selectedCT);
           }
           return bd;
       }
   }
}
