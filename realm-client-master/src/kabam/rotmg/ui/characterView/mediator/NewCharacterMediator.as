package kabam.rotmg.ui.characterView.mediator {
import kabam.rotmg.classes.model.ClassesModel;
import kabam.rotmg.game.model.GameInitData;
import kabam.rotmg.game.signals.PlayGameSignal;
import kabam.rotmg.ui.characterView.NewCharacterScreen;

import robotlegs.bender.bundles.mvcs.Mediator;

public class NewCharacterMediator extends Mediator{

    [Inject]
    public var view:NewCharacterScreen;
    [Inject]
    public var play:PlayGameSignal;
    [Inject]
    public var classesModel:ClassesModel;

    public function NewCharacterMediator() {
        super();
    }

    override public function initialize() : void
    {
        this.view.onCreate.addOnce(onPlay);
    }

    public function onPlay(objType:uint):void {
        this.classesModel.getCharacterClass(objType).setIsSelected(true);
        var game:GameInitData = new GameInitData();
        game.createCharacter = true;
        game.isNewGame = true;
        this.play.dispatch(game);
    }
}
}
