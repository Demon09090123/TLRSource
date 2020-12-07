package com.company.assembleegameclient.screens.charrects
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.util.AnimatedChar;
import com.company.assembleegameclient.util.AnimatedChars;

import flash.display.Bitmap;
   import flash.display.BitmapData;
   import flash.display.DisplayObject;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
   import kabam.rotmg.assets.services.CharacterFactory;
   import kabam.rotmg.classes.model.CharacterClass;
   import kabam.rotmg.classes.model.CharacterSkin;
   import kabam.rotmg.classes.model.ClassesModel;
   import kabam.rotmg.core.StaticInjectorContext;
   import kabam.rotmg.core.model.PlayerModel;
   import org.osflash.signals.Signal;
   import org.swiftsuspenders.Injector;
   
   public class CharacterRectList extends Sprite
   {
      private var classes:ClassesModel;
      private var model:PlayerModel;
      private var assetFactory:CharacterFactory;
      public var newCharacter:Signal;

      public function CharacterRectList(pModel:PlayerModel) {
          this.model = pModel;
          var hasCharacter:Boolean = this.model.hasCharacter();

          var injector:Injector = StaticInjectorContext.getInjector();
          this.classes = injector.getInstance(ClassesModel);
          this.assetFactory = injector.getInstance(CharacterFactory);
          this.newCharacter = new Signal();

          if (hasCharacter) {
              createCurrent();
          } else {
              createNew();
          }
      }

      private function createNew():void {
          var newRect:CreateNewCharacterRect = new CreateNewCharacterRect();
          //newRect.addEventListener(MouseEvent.MOUSE_DOWN, this.onNewChar);

          addChild(newRect);
      }

      private function createCurrent():void {
          var curRect:CurrentCharacterRect = new CurrentCharacterRect(this.model);
          curRect.setIcon(this.getIcon(this.model.getSavedCharacter()));
          addChild(curRect);
      }

      private function getIcon(savedChar:SavedCharacter) : DisplayObject
      {
         var type:CharacterClass = this.classes.getCharacterClass(savedChar.objectType());
         var skin:CharacterSkin = type.skins.getSkin(savedChar.skinType()) || type.skins.getDefaultSkin();
         var data:BitmapData = this.assetFactory.makeIcon(skin.template,100,savedChar.tex1(),savedChar.tex2(), 0, 2);
         return new Bitmap(data);
      }
      
      private function onNewChar(event:Event) : void
      {
         this.newCharacter.dispatch();
      }
   }
}
