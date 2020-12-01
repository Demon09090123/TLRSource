package com.company.assembleegameclient.screens.charrects
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
   import com.company.assembleegameclient.objects.ObjectLibrary;
   import com.company.assembleegameclient.util.AnimatedChar;
   import com.company.assembleegameclient.util.FameUtil;
   import com.company.rotmg.graphics.StarGraphic;
   import com.company.ui.SimpleText;
   import com.company.util.BitmapUtil;
   import flash.display.Bitmap;
   import flash.display.BitmapData;
   import flash.display.Sprite;
   import flash.filters.DropShadowFilter;
   import flash.geom.ColorTransform;
   import kabam.rotmg.core.model.PlayerModel;
   
   public class CreateNewCharacterRect extends Sprite
   {
      public function CreateNewCharacterRect(model:PlayerModel)
      {

          var name:SimpleText = new SimpleText(28, 0xFFFFFF).setBold(true);
          name.text = "Create a New Character";
          name.updateMetrics();
          name.y = 0;

          addChild(name);

          var xml:XML = ObjectLibrary.playerChars_[int (ObjectLibrary.playerChars_.length * Math.random() )];
          var data:BitmapData = SavedCharacter.getImage(null, xml, AnimatedChar.DOWN, AnimatedChar.STAND, 0, false, true);

          var bitMap:Bitmap = new Bitmap(data);
          bitMap.scaleX = 3;
          bitMap.scaleY = 3;
          bitMap.y = 30;
          bitMap.x = (width - bitMap.width) / 2;

          this.addChild(bitMap);
      }
   }
}
