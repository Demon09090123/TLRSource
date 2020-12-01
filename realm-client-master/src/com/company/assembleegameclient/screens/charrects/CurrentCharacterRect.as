package com.company.assembleegameclient.screens.charrects
{
   import com.company.assembleegameclient.appengine.SavedCharacter;
   import com.company.assembleegameclient.ui.tooltip.MyPlayerToolTip;
   import com.company.assembleegameclient.ui.tooltip.ToolTip;
   import com.company.assembleegameclient.util.FameUtil;
   import com.company.rotmg.graphics.StarGraphic;
   import com.company.ui.SimpleText;
   import flash.display.DisplayObject;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
   import flash.filters.DropShadowFilter;
   import flash.geom.ColorTransform;
   import kabam.rotmg.classes.model.CharacterClass;
   import org.osflash.signals.Signal;
   import org.osflash.signals.natives.NativeMappedSignal;
   
   public class CurrentCharacterRect extends Sprite
   {
      public var charName:String;
      private var charClass:CharacterClass;
      public var savedChar:SavedCharacter;
      public var selected:Signal;
      private var icon:DisplayObject;

      public function CurrentCharacterRect(name:String, charClass:CharacterClass, char:SavedCharacter) {
          this.charName = name;
          this.charClass = charClass;
          this.savedChar = char;

          this.selected = new NativeMappedSignal(this, MouseEvent.CLICK).mapTo(char);

          makeName();
      }

      private function makeName():void {
          var classText:SimpleText = new SimpleText(32, 0xFFFFFF).setBold(true);
          classText.text = charClass.name;
          classText.updateMetrics();

          var nameText:SimpleText = new SimpleText(28, 0xFFFFFF).setBold(false);
          nameText.text = "Level " + this.savedChar.level();
          nameText.updateMetrics();
          nameText.y = 30;

          addChild(classText);
          addChild(nameText);
      }

      public function setIcon(value:DisplayObject) : void
      {
         this.icon && removeChild(this.icon);
         this.icon = value;
         this.icon.x = 0;
         this.icon.y = 60;
         this.icon && addChild(this.icon);
      }

      protected function onMouseOver(event:MouseEvent) : void
      {
          if (this.icon) {
              this.icon.alpha = .5;
          }
      }

      protected function onRollOut(event:MouseEvent) : void
      {
          if (this.icon) {
              this.icon.alpha = 1;
          }
      }
   }
}
