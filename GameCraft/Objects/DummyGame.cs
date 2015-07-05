using System.Collections.Generic;
using GameCraft.Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework.Constraints;

namespace GameCraft
{
    public class DummyGame
    {
        public DummyGame()
        {
            List<Rule> RuleList = new List<Rule>();
            SceneObject alex = new SceneObject("Alex");
            alexObject = alex;

            GraphicContent blueMageStanding = new GraphicContent("Wizard_Standing", 1, 1, 0, 1, 0.5f);
            BlueMageStanding = blueMageStanding;
            
            PropertyCondition ifNotVisible = new PropertyCondition("If GameObject Alex is not Visible", "Alex","Visible", Operator.NotEqual, true);
            ObjectAction makeVisisble = new ObjectAction("Make Alex Visible", "Alex", "Visible", true, Operation.Set);
            ObjectAction setDefaultAnimation = new ObjectAction("Set Default Animation for Alex", "Alex", "DefaultAnimation", "Wizard_Standing", Operation.Set);
            ObjectAction setAnimation = new ObjectAction("Set Main Animation for Alex", "Alex", "Animation", "Wizard_Standing", Operation.Set);

            InputCondition<Keys, KeyState> ifUpIsPressed = new InputCondition<Keys, KeyState>("If the Down Key is pressed", Keys.Down, KeyState.Down, Operator.Equal);
            ObjectAction moveAlexUp = new ObjectAction("Move Alex up 2 pixles", "Alex", "PositionY", 2, Operation.Add);

            Rule IfUpIsPressed_MoveAlexUp = new Rule("If the Down Key is pressed, Move Alex up 2 pixles");

            IfUpIsPressed_MoveAlexUp.KeyInputConditions.Add(ifUpIsPressed.Name, ifUpIsPressed);
            IfUpIsPressed_MoveAlexUp.ObjectActions.Add(moveAlexUp.Name, moveAlexUp);


            
            
            Rule ifNotVisible_MakeVisible = new Rule("If Alex is not Visisble, make him Visisble");
            ifNotVisible_MakeVisible.PropertyConditions.Add(ifNotVisible.Name, ifNotVisible);
            
            ifNotVisible_MakeVisible.ObjectActions.Add(makeVisisble.Name, makeVisisble);
            ifNotVisible_MakeVisible.ObjectActions.Add(setDefaultAnimation.Name, setDefaultAnimation);
            ifNotVisible_MakeVisible.ObjectActions.Add(setAnimation.Name, setAnimation);

            RuleList.Add(ifNotVisible_MakeVisible);

            PropertyCondition ifVisible = new PropertyCondition("If GameObject Alex is Visible", "Alex", "Visible", Operator.Equal, true);
            GraphicAction drawWizard = new GraphicAction("Draw Wizard","Alex", new PlayCount(1, true));

            Rule ifVisible_DrawToScreen_SetAnimation = new Rule("If Alex is visisble, draw him on the screen");
            ifVisible_DrawToScreen_SetAnimation.PropertyConditions.Add(ifVisible.Name, ifVisible);
            ifVisible_DrawToScreen_SetAnimation.GraphicActions.Add(drawWizard.Name, drawWizard);
            
            RuleList.Add(ifVisible_DrawToScreen_SetAnimation);
            GameState = new State("Test Game State");

            GameState.Rules.Add(ifVisible_DrawToScreen_SetAnimation.Name, ifVisible_DrawToScreen_SetAnimation);
            GameState.Rules.Add(ifNotVisible_MakeVisible.Name, ifNotVisible_MakeVisible);
            GameState.Rules.Add(IfUpIsPressed_MoveAlexUp.Name, IfUpIsPressed_MoveAlexUp);
        }

        public State GameState { get; set; }

        public GameObject alexObject { get; set; }

        public GraphicContent BlueMageStanding { get; set; }

        

    }
}