package kabam.rotmg.classes.view
{
   import kabam.rotmg.classes.model.ClassesModel;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class CharacterSkinListItemMediator extends Mediator
   {
       
      
      [Inject]
      public var view:CharacterSkinListItem;
      
      [Inject]
      public var model:ClassesModel;

      public function CharacterSkinListItemMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.view.selected.add(this.onSelected);
      }
      
      override public function destroy() : void
      {
         this.view.selected.remove(this.onSelected);
         this.view.setModel(null);
      }

      
      private function onSelected(isSelected:Boolean) : void
      {
         this.view.getModel().setIsSelected(isSelected);
      }
   }
}
