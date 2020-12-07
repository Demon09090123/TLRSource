package com.company.assembleegameclient.ui
{
   import com.company.ui.SimpleText;
   import flash.display.Sprite;
   
   public class StatusBar extends Sprite
   {
      public var w_:int;
      public var h_:int;
      public var color_:uint;
      public var backColor_:uint;
      
      public var val_:int = -1;
      public var max_:int = -1;
      
      public var labelText_:SimpleText;
      public var valueText_:SimpleText;

      public var colorSprite:Sprite;

      private var outline:Boolean;
      
      public function StatusBar(w:int, h:int, color:uint, backColor:uint, valTextClr:uint = 164777215, outline:Boolean = false,
                                label:String = null)
      {
         super();

         this.colorSprite = new Sprite();

         this.w_ = w;
         this.h_ = h;
         this.color_ = color;
         this.backColor_ = backColor;
         this.outline = outline;

         this.valueText_ = new SimpleText(14, valTextClr).setBold(true);
         this.valueText_.x = 6;


          if (this.backColor_ != -1) {
              graphics.beginFill(this.backColor_);
              graphics.drawRect(0, 0, w_, h_);
              graphics.endFill();
          }

         addChild(this.colorSprite);
         drawLabel(label);
         addChild(this.valueText_);
      }

      private function drawLabel(label:String):void {
          if(label != null && label.length != 0){
              this.labelText_ = new SimpleText(14, 16777215).setBold(true);
              this.labelText_.text = label;
              this.labelText_.updateMetrics();
              //this.labelText_.y = (h_ - this.labelText_.height) / 2;
              if (this.outline) {
                  this.labelText_.addOutline(2)
              }

              addChild(this.labelText_);

              this.valueText_.x = this.labelText_.width + 5;
          }
      }
      
      public function draw(val:int, max:int, drawValue:Boolean = true) : void {
         if(max > 0) {
            val = Math.min(max,Math.max(0,val));
         }
         if(val == this.val_ && max == this.max_) {
            return;
         }
         this.val_ = val;
         this.max_ = max;
         this.internalDraw(drawValue);
      }

      private function internalDraw(drawValue:Boolean = true) : void {
          this.colorSprite.graphics.clear();
          this.colorSprite.graphics.beginFill(this.color_);

          if(this.max_ > 0) {
              this.colorSprite.graphics.drawRect(0, 0, this.w_ * (this.val_ / this.max_),this.h_);
          } else {
              this.colorSprite.graphics.drawRect(0,0,this.w_,this.h_);
          }
          this.colorSprite.graphics.endFill();

          if(this.max_ > 0) {
              this.valueText_.text = "" + this.val_ + "/" + this.max_;
          } else {
              this.valueText_.text = "" + this.val_;
          }

          if (drawValue == false) {
              this.valueText_.text = "";
          }

          this.valueText_.updateMetrics();
          this.valueText_.y = (h_ - this.valueText_.height) / 2;

          if (outline && drawValue) {
              this.valueText_.removeOutline();
              this.valueText_.addOutline(2, 0);
          }
      }
   }
}
