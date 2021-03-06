package kabam.rotmg.classes.view
{
   import com.company.assembleegameclient.util.FameUtil;
   import flash.events.TimerEvent;
   import flash.utils.Timer;
   import kabam.rotmg.assets.model.Animation;
   import kabam.rotmg.assets.services.CharacterFactory;
   import kabam.rotmg.classes.model.CharacterClass;
   import kabam.rotmg.classes.model.CharacterSkin;
   import kabam.rotmg.classes.model.ClassesModel;
   import kabam.rotmg.core.model.PlayerModel;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class ClassDetailMediator extends Mediator
   {
       
      
      [Inject]
      public var view:ClassDetailView;
      
      [Inject]
      public var classesModel:ClassesModel;
      
      [Inject]
      public var playerModel:PlayerModel;

      [Inject]
      public var factory:CharacterFactory;
      
      private const skins:Object = new Object();
      
      private var character:CharacterClass;
      
      private var nextSkin:CharacterSkin;
      
      private const nextSkinTimer:Timer = new Timer(250,1);
      
      public function ClassDetailMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.character = this.classesModel.getSelected();
         this.nextSkinTimer.addEventListener(TimerEvent.TIMER,this.delayedFocusSet);
         this.setCharacterData();
         this.onFocusSet();
      }
      
      override public function destroy() : void
      {
         this.nextSkinTimer.removeEventListener(TimerEvent.TIMER,this.delayedFocusSet);
         this.view.setWalkingAnimation(null);
         this.disposeAnimations();
      }
      
      private function setCharacterData() : void
      {
         var level:int = this.playerModel.getLevel();
         var stars:int = FameUtil.numStars(level);
         this.view.setData(this.character.name,this.character.description,stars, level);
         var nextStarFame:int = FameUtil.nextStarFame();
         this.view.setNextGoal(this.character.name,nextStarFame);
      }
      
      private function onFocusSet(skin:CharacterSkin = null) : void
      {
         this.nextSkin = skin = skin || this.character.skins.getSelectedSkin();
         this.nextSkinTimer.start();
      }
      
      private function delayedFocusSet(e:TimerEvent) : void
      {
         var animation:Animation = this.skins[this.nextSkin.id] = this.skins[this.nextSkin.id] || this.factory.makeWalkingIcon(this.nextSkin.template,200);
         this.view.setWalkingAnimation(animation);
      }
      
      private function disposeAnimations() : void
      {
         var id:* = null;
         var animation:Animation = null;
         for(id in this.skins)
         {
            animation = this.skins[id];
            animation.dispose();
            delete this.skins[id];
         }
      }
   }
}
