package kabam.rotmg.game.view
{
   import kabam.rotmg.core.model.PlayerModel;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class CreditDisplayMediator extends Mediator
   {
       
      
      [Inject]
      public var view:CreditDisplay;
      
      [Inject]
      public var model:PlayerModel;
      
      public function CreditDisplayMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.model.creditsChanged.add(this.onCreditsChanged);
         this.view.draw(this.model.getCredits());
      }
      
      override public function destroy() : void
      {
         this.model.creditsChanged.remove(this.onCreditsChanged);
      }
      
      private function onCreditsChanged(credits:int) : void
      {
         this.view.draw(credits);
      }
   }
}
