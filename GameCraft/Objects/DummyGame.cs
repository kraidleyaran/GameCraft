using System.Collections.Generic;
using GameCraft.Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameCraft
{
    public class DummyGame
    {
        public DummyGame()
        {
            List<Rule> RuleList = new List<Rule>();
            SceneObject collide = new SceneObject("Collide", "Scene");
            SceneObject alex = new SceneObject("Alex", "Scene");
            alexObject = alex;
            collideObject = collide;

            GraphicContent blueMageStanding = new GraphicContent("RedBlock", 1, 1, 0, 1, 0.5f);
            GraphicContent blueMageRightWalking = new GraphicContent("GrayBox", 1, 1, 0, 1, 0.5f);
            BlueMageStanding = blueMageStanding;
            BlueMageRightWalking = blueMageRightWalking;
            
            PropertyCondition ifNotVisible = new PropertyCondition("If GameObject Alex is not Visible", "Alex","Visible", Operator.NotEqual, true);
            ObjectAction makeVisisble = new ObjectAction("Make Alex Visible", "Alex", "Visible", true, Operation.Set);
            ObjectAction setDefaultAnimation = new ObjectAction("Set Default Animation for Alex", "Alex", "DefaultAnimation", "RedBlock", Operation.Set);
            ObjectAction setAnimation = new ObjectAction("Set Main Animation for Alex", "Alex", "Animation", "RedBlock", Operation.Set);

            PropertyCondition ifNotVisible_Collide = new PropertyCondition("If GameObject Collide is not Visible", "Collide", "Visible", Operator.NotEqual, true);
            ObjectAction makeVisisble_Collide = new ObjectAction("Make Alex Visible", "Collide", "Visible", true, Operation.Set);
            ObjectAction setDefaultAnimation_Collide = new ObjectAction("Set Default Animation for Collide", "Collide", "DefaultAnimation", "GrayBox", Operation.Set);
            ObjectAction setAnimation_Collide = new ObjectAction("Set Main Animation for Collide", "Collide", "Animation", "GrayBox", Operation.Set);

            KeyboardCondition ifDownIsPressed = new KeyboardCondition("If the Down Key is pressed", Keys.Down, KeyState.Down, Operator.Equal);
            ObjectAction moveAlexDown = new ObjectAction("Move Alex down 2 pixles", "Alex", "PositionY", 2, Operation.Add);
            CollisionAction moveAlex_Collision = new CollisionAction("Move Alex's collision box", "Alex", CollisionActionOperation.Move);

            KeyboardCondition ifUpIsPressed  = new KeyboardCondition("If the Up Key is pressed", Keys.Up, KeyState.Down, Operator.Equal);
            ObjectAction moveAlexUp = new ObjectAction("Move Alex up 2 pixles", "Alex", "PositionY", 2, Operation.Subtract);

            KeyboardCondition ifLeftIsPressed = new KeyboardCondition("If the Left Key is pressed", Keys.Left, KeyState.Down, Operator.Equal);
            ObjectAction moveAlexLeft = new ObjectAction("Move Alex Left 2 pixles", "Alex", "PositionX", 2, Operation.Subtract);

            KeyboardCondition ifRightIsPressed = new KeyboardCondition("If the Right Key is pressed", Keys.Right, KeyState.Down, Operator.Equal);
            ObjectAction moveAlexRight = new ObjectAction("Move Alex Right 2 pixles", "Alex", "PositionX", 2, Operation.Add);

            Rule IfDownIsPressed_MoveAlexDown = new Rule("If the Down Key is pressed, Move Alex down 2 pixles");
            Rule IfUpIsPressed_MoveAlexUp = new Rule("If the UP Key is pressed, Move Alex up 2 pixels");
            Rule IfLeftIsPressed_MoveAlexLeft = new Rule("If Left is pressed, MOve Alex left 2 pixels");
            Rule IfRightIsPresed_MoveAlexRight = new Rule("If Right is pressed, move Alex right 2 pixels");

            IfUpIsPressed_MoveAlexUp.KeyInputConditions.Add(ifUpIsPressed.Name, ifUpIsPressed);
            IfUpIsPressed_MoveAlexUp.ObjectActions.Add(moveAlexUp.Name, moveAlexUp);
            IfUpIsPressed_MoveAlexUp.CollisionActions.Add(moveAlex_Collision.Name, moveAlex_Collision);

            IfLeftIsPressed_MoveAlexLeft.KeyInputConditions.Add(ifLeftIsPressed.Name, ifLeftIsPressed);
            IfLeftIsPressed_MoveAlexLeft.ObjectActions.Add(moveAlexLeft.Name, moveAlexLeft);
            IfLeftIsPressed_MoveAlexLeft.CollisionActions.Add(moveAlex_Collision.Name, moveAlex_Collision);

            IfRightIsPresed_MoveAlexRight.KeyInputConditions.Add(ifRightIsPressed.Name, ifRightIsPressed);
            IfRightIsPresed_MoveAlexRight.ObjectActions.Add(moveAlexRight.Name, moveAlexRight);
            IfRightIsPresed_MoveAlexRight.CollisionActions.Add(moveAlex_Collision.Name, moveAlex_Collision);

            IfDownIsPressed_MoveAlexDown.KeyInputConditions.Add(ifDownIsPressed.Name, ifDownIsPressed);
            IfDownIsPressed_MoveAlexDown.ObjectActions.Add(moveAlexDown.Name, moveAlexDown);
            IfDownIsPressed_MoveAlexDown.CollisionActions.Add(moveAlex_Collision.Name, moveAlex_Collision);


            Rule ifNotVisible_MakeVisible = new Rule("If Alex is not Visisble, make him Visisble");
            Rule ifNotVisible_MakeVisible_Collide = new Rule("If Collide is not Visisble, make him Visisble");

            ifNotVisible_MakeVisible.PropertyConditions.Add(ifNotVisible.Name, ifNotVisible);

            
            ifNotVisible_MakeVisible.ObjectActions.Add(makeVisisble.Name, makeVisisble);
            ifNotVisible_MakeVisible.ObjectActions.Add(setDefaultAnimation.Name, setDefaultAnimation);
            ifNotVisible_MakeVisible.ObjectActions.Add(setAnimation.Name, setAnimation);

            ifNotVisible_MakeVisible_Collide.PropertyConditions.Add(ifNotVisible_Collide.Name, ifNotVisible_Collide);

            ifNotVisible_MakeVisible_Collide.ObjectActions.Add(ifNotVisible_MakeVisible_Collide.Name, makeVisisble_Collide);
            ifNotVisible_MakeVisible_Collide.ObjectActions.Add(setDefaultAnimation_Collide.Name, setDefaultAnimation_Collide);
            ifNotVisible_MakeVisible_Collide.ObjectActions.Add(setAnimation_Collide.Name, setAnimation_Collide);

            RuleList.Add(ifNotVisible_MakeVisible);
            RuleList.Add(ifNotVisible_MakeVisible_Collide);

            PropertyCondition ifVisible = new PropertyCondition("If GameObject Alex is Visible", "Alex", "Visible", Operator.Equal, true);
            GraphicAction drawWizard = new GraphicAction("Draw Wizard","Alex", new PlayCount(1, true));
            CollisionAction AddCollisionObject_Alex = new CollisionAction("Add Alex to tree", "Alex", CollisionActionOperation.Add);

            PropertyCondition ifVisible_Collide = new PropertyCondition("If GameObject Collide is Visible", "Collide", "Visible", Operator.Equal, true);
            ObjectAction SetPosition_collide = new ObjectAction("Set coordinates for Collide", "Collide","PositionY",500, Operation.Set);
            CollisionAction AddCollisionObject_Collsion = new CollisionAction("Add Collide to tree", "Collide", CollisionActionOperation.Add);
            GraphicAction drawWizard_Collide = new GraphicAction("Draw Collide", "Collide", new PlayCount(1, true));

            Rule ifVisible_DrawToScreen_SetAnimation = new Rule("If Alex is visisble, draw him on the screen");
            ifVisible_DrawToScreen_SetAnimation.PropertyConditions.Add(ifVisible.Name, ifVisible);
            ifVisible_DrawToScreen_SetAnimation.GraphicActions.Add(drawWizard.Name, drawWizard);
            ifVisible_DrawToScreen_SetAnimation.CollisionActions.Add(AddCollisionObject_Alex.Name, AddCollisionObject_Alex);

            Rule ifVisible_DrawToScreen_SetAnimation_Collide = new Rule("If Collide is visisble, draw him on the screen");
            ifVisible_DrawToScreen_SetAnimation_Collide.PropertyConditions.Add(ifVisible_Collide.Name, ifVisible_Collide);
            ifVisible_DrawToScreen_SetAnimation_Collide.GraphicActions.Add(drawWizard_Collide.Name, drawWizard_Collide);
            ifVisible_DrawToScreen_SetAnimation_Collide.ObjectActions.Add(SetPosition_collide.Name, SetPosition_collide);
            ifVisible_DrawToScreen_SetAnimation_Collide.CollisionActions.Add(AddCollisionObject_Collsion.Name, AddCollisionObject_Collsion);
            
            RuleList.Add(ifVisible_DrawToScreen_SetAnimation);
            RuleList.Add(ifVisible_DrawToScreen_SetAnimation_Collide);
            Level testLevel = new Level("Test Level", new LevelSize(9000f,9000f,2));
            GameState = new State("Test Game State", testLevel);

            GameState.Rules.Add(ifVisible_DrawToScreen_SetAnimation.Name, ifVisible_DrawToScreen_SetAnimation);
            GameState.Rules.Add(ifNotVisible_MakeVisible.Name, ifNotVisible_MakeVisible);
            GameState.Rules.Add(IfUpIsPressed_MoveAlexUp.Name, IfUpIsPressed_MoveAlexUp);
            GameState.Rules.Add(IfLeftIsPressed_MoveAlexLeft.Name, IfLeftIsPressed_MoveAlexLeft);
            GameState.Rules.Add(IfRightIsPresed_MoveAlexRight.Name, IfRightIsPresed_MoveAlexRight);
            GameState.Rules.Add(IfDownIsPressed_MoveAlexDown.Name, IfDownIsPressed_MoveAlexDown);

            GameState.Rules.Add(ifVisible_DrawToScreen_SetAnimation_Collide.Name, ifVisible_DrawToScreen_SetAnimation_Collide);
            GameState.Rules.Add(ifNotVisible_MakeVisible_Collide.Name, ifNotVisible_MakeVisible_Collide);

            CollisionCondition ifCollide_And_Alex_Collide = new CollisionCondition("If Collide and Alex collide", "Alex", CollisionOperator.DidCollide, "Collide");
            ObjectAction SetCollideToTop = new ObjectAction("Set Alex to Top", "Alex", "PositionY", 0, Operation.Set);
            Rule IfCollideAndAlexCollide_SetCollideToTop = new Rule("If Collide and Alex collide, Set Alex to Top");
            IfCollideAndAlexCollide_SetCollideToTop.CollisionConditions.Add(ifCollide_And_Alex_Collide.Name, ifCollide_And_Alex_Collide);
            IfCollideAndAlexCollide_SetCollideToTop.ObjectActions.Add(SetCollideToTop.Name, SetCollideToTop);
            IfCollideAndAlexCollide_SetCollideToTop.CollisionActions.Add(moveAlex_Collision.Name, moveAlex_Collision);

            GameState.Rules.Add(IfCollideAndAlexCollide_SetCollideToTop.Name, IfCollideAndAlexCollide_SetCollideToTop);


        }

        public State GameState { get; set; }

        public GameObject alexObject { get; set; }

        public GameObject collideObject { get; set; }

        public GraphicContent BlueMageStanding { get; set; }

        public GraphicContent BlueMageRightWalking { get; set; }

        

    }
}