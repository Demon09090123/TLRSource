package com.company.assembleegameclient.screens
{
   import com.company.assembleegameclient.screens.charrects.CharacterRectList;
   import flash.display.Graphics;
   import flash.display.Shape;
   import flash.display.Sprite;
   import kabam.rotmg.core.model.PlayerModel;
   
   public class CharacterList extends Sprite
   {
      
      public static const WIDTH:int = 800;
      
      public static const HEIGHT:int = 600;
       
      
      public var charRectList_:CharacterRectList;
      
      public function CharacterList(model:PlayerModel)
      {
         super();
         this.charRectList_ = new CharacterRectList(model);
         addChild(this.charRectList_);
      }
   }
}
