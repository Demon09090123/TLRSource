package drawing.components
{
   import com.company.assembleegameclient.sound.SoundEffectLibrary;
   import com.company.ui.SimpleText;
   import com.company.util.MoreColorUtil;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.events.MouseEvent;
import flash.filters.BitmapFilterQuality;
import flash.filters.BlurFilter;
import flash.filters.DropShadowFilter;
import flash.filters.GlowFilter;
import flash.geom.ColorTransform;
   import flash.utils.getTimer;
   import org.osflash.signals.Signal;
import org.osmf.net.qos.QualityLevel;

public class TitleMenuOption extends Sprite
   {
      protected static const OVER_COLOR_TRANSFORM:ColorTransform = new ColorTransform(1,220 / 255,133 / 255);

      public const clicked:Signal = new Signal();
      private var colorTransform:ColorTransform;
      private var size:int;
      private var textField:SimpleText;
      private var isPulse:Boolean;
      private var active:Boolean;
      private var originalWidth:Number;
      private var originalHeight:Number;

      private var bold:Boolean;
      private var txtClr:uint;
      
      public function TitleMenuOption(text:String, size:int = 18, pulse:Boolean = false, bold:Boolean = true, txtClr:uint = 16777215)
      {
         super();
         this.size = size;
          this.bold = bold;
          this.txtClr = txtClr;
         this.setText(text);
         this.isPulse = pulse;
         this.originalWidth = width;
         this.originalHeight = height;
         activate();
      }
      
      public function setText(text:String) : void
      {
         name = text;
         if(this.textField)
         {
            removeChild(this.textField);
         }
         this.textField = new SimpleText(this.size,txtClr,false,0,0);
         this.textField.setBold(bold);
         this.textField.text = text;
         this.textField.updateMetrics();
         addChild(this.textField);
      }

      public function activate():void
      {
          setColorTransform(null);
         addEventListener(MouseEvent.MOUSE_OVER,this.onMouseOver);
         addEventListener(MouseEvent.MOUSE_OUT,this.onMouseOut);
         addEventListener(MouseEvent.CLICK,this.onMouseClick);
         addEventListener(Event.ADDED_TO_STAGE,this.onAddedToStage);
         addEventListener(Event.REMOVED_FROM_STAGE,this.onRemovedFromStage);
         this.active = true;
      }

      public function deactivate():void {
         var ct:ColorTransform = new ColorTransform();
         ct.color = 0x363636;
         setColorTransform(ct);
         removeEventListener(MouseEvent.MOUSE_OVER,this.onMouseOver);
         removeEventListener(MouseEvent.MOUSE_OUT,this.onMouseOut);
         removeEventListener(MouseEvent.CLICK,this.onMouseClick);
         removeEventListener(Event.ADDED_TO_STAGE,this.onAddedToStage);
         removeEventListener(Event.REMOVED_FROM_STAGE,this.onRemovedFromStage);
      }

       public function addOutline(thickness:int = 1, clr:uint = 0):void {
           var filter:GlowFilter = new GlowFilter(clr, 1, thickness, thickness, thickness * 5, BitmapFilterQuality.HIGH);
           filters = [filter];
       }

       public function removeOutline():void {
           filters = [];
       }
      
      private function onAddedToStage(event:Event) : void
      {
         if(this.isPulse)
         {
            addEventListener(Event.ENTER_FRAME,this.onEnterFrame);
         }
      }
      
      private function onRemovedFromStage(event:Event) : void
      {
         if(this.isPulse)
         {
            removeEventListener(Event.ENTER_FRAME,this.onEnterFrame);
         }
      }
      
      private function onEnterFrame(event:Event) : void
      {
         var s:Number = 1.05 + 0.05 * Math.sin(getTimer() / 200);
         this.textField.scaleX = s;
         this.textField.scaleY = s;
         this.textField.x = this.originalWidth / 2 - this.textField.width / 2;
         this.textField.y = this.originalHeight / 2 - this.textField.height / 2;
      }
      
      public function setColorTransform(ct:ColorTransform) : void
      {
         if(ct == this.colorTransform)
         {
            return;
         }
         this.colorTransform = ct;
         if(this.colorTransform == null)
         {
            this.textField.transform.colorTransform = MoreColorUtil.identity;
         }
         else
         {
            this.textField.transform.colorTransform = this.colorTransform;
         }
      }
      
      protected function onMouseOver(event:MouseEvent) : void
      {
         this.setColorTransform(OVER_COLOR_TRANSFORM);
      }
      
      protected function onMouseOut(event:MouseEvent) : void
      {
         this.setColorTransform(null);
      }
      
      protected function onMouseClick(event:MouseEvent) : void
      {
         SoundEffectLibrary.play("button_click");
         this.clicked.dispatch();
      }
   }
}