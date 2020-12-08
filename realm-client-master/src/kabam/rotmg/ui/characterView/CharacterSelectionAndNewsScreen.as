package kabam.rotmg.ui.characterView
{
import com.company.assembleegameclient.appengine.SavedCharacter;
import drawing.screen.DarkLayer;
import drawing.uiAssets.UIComponentHelper;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.display.DisplayObject;
import flash.display.Sprite;
import flash.events.MouseEvent;

import kabam.rotmg.assets.services.CharacterFactory;
import kabam.rotmg.classes.model.CharacterClass;
import kabam.rotmg.classes.model.CharacterSkin;
import kabam.rotmg.classes.model.ClassesModel;
import kabam.rotmg.core.StaticInjectorContext;
import kabam.rotmg.core.model.PlayerModel;

import org.swiftsuspenders.Injector;

import starling.filters.GlowFilter;

public class CharacterSelectionAndNewsScreen extends Sprite
   {
      private var isInitialized:Boolean;
      private var model:PlayerModel;
      
      public function CharacterSelectionAndNewsScreen()
      {
         super();

         addChild(UIComponentHelper._charScreen);
         addChild(new DarkLayer());
      }
      
      public function initialize(model:PlayerModel) : void
      {
         if(this.isInitialized)
         {
            return;
         }
         this.isInitialized = true;
         this.model = model;
         this.createDisplayAssets();
      }
      
      private function createDisplayAssets() : void
      {
         this.createCharacterList();
      }

      private function createCharacterList() : void
      {
         if (model.hasCharacter()) {
             var curRect:CurrentCharacterScreen = new CurrentCharacterScreen(this.model);
             addChild(curRect);
         } else {
             var newRect:NewCharacterScreen = new NewCharacterScreen();
             addChild(newRect);
         }
      }
   }
}
