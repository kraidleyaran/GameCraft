using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameCraft
{
    public class CollisionManager
    {
        private Dictionary<string, QuadTreePositionItem<GameObject>> _positionItems = new Dictionary<string, QuadTreePositionItem<GameObject>>();
        public CollisionManager(QuadTree<GameObject> newTree)
        {
            CurrentTree = newTree;
        }

        public QuadTree<GameObject> CurrentTree { get; set; }

        public bool Add(GameObject gameObject)
        {
            List<QuadTreePositionItem<GameObject>> itemList = new List<QuadTreePositionItem<GameObject>>();
            CurrentTree.GetAllItems(ref itemList);
            if (itemList.Find(item => item.Parent.Name == gameObject.Name) != null)
            {
                return false;
            }
            
            List<string> getValueList = new List<string>
            {
                "PositionX",
                "PositionY",
                "Height",
                "Width"
            };
            Receipt<List<GameObjectProperty>> getValuesReceipt = gameObject.GetManyProperty(getValueList);

            if (getValuesReceipt.Failures.Count > 0)
            {
                return false;
            }

            Vector2 position = new Vector2();
            Vector2 size = new Vector2();

            position.X = Convert.ToSingle(getValuesReceipt.Response.Find(prop => prop.Name == "PositionX").Value);
            position.Y = Convert.ToSingle(getValuesReceipt.Response.Find(prop => prop.Name == "PositionY").Value);
            size.X = Convert.ToSingle(getValuesReceipt.Response.Find(prop => prop.Name == "Width").Value);
            size.Y = Convert.ToSingle(getValuesReceipt.Response.Find(prop => prop.Name == "Height").Value);
            QuadTreePositionItem<GameObject> newPositionItem = new QuadTreePositionItem<GameObject>(gameObject, position,size);
            CurrentTree.Insert(newPositionItem);
            _positionItems.Add(gameObject.Name, newPositionItem);
            return true;

        }

        public bool Remove(GameObject gameObject)
        {
            List<QuadTreePositionItem<GameObject>> itemList = new List<QuadTreePositionItem<GameObject>>();
            CurrentTree.GetAllItems(ref itemList);
            QuadTreePositionItem<GameObject> currentObject = itemList.Find(item => item.Parent.Name == gameObject.Name);
            if (currentObject == null)
            {
                return false;
            }
            CurrentTree.HeadNode.RemoveItem(currentObject);
            return true;
        }

        public bool Move(GameObject gameObject)
        {
            if (!(_positionItems.ContainsKey(gameObject.Name)))
            {
                return false;
            }
            List<QuadTreePositionItem<GameObject>> itemList = new List<QuadTreePositionItem<GameObject>>();
            CurrentTree.GetAllItems(ref itemList);
            QuadTreePositionItem<GameObject> currentObject = itemList.Find(item => item.Parent.Name == gameObject.Name);

            List<string> getValueList = new List<string>
            {
                "PositionX",
                "PositionY",
                "Height",
                "Width"
            };
            Receipt<List<GameObjectProperty>> getValueReceipt = gameObject.GetManyProperty(getValueList);
            if (getValueReceipt.Failures.Count > 0)
            {
                return false;
            }

            Vector2 position = new Vector2(Convert.ToSingle(getValueReceipt.Response.Find(prop => prop.Name == "PositionX").Value), Convert.ToSingle(getValueReceipt.Response.Find(prop => prop.Name == "PositionY").Value));
            Vector2 size = new Vector2(Convert.ToSingle(getValueReceipt.Response.Find(prop => prop.Name == "Width").Value), Convert.ToSingle(getValueReceipt.Response.Find(prop => prop.Name == "Height").Value));

            currentObject.Position = position;
            currentObject.Size = size;

            //CurrentTree.HeadNode.ItemMove(currentObject);
            _positionItems.Remove(gameObject.Name);
            _positionItems.Add(gameObject.Name, currentObject);
            return true;
        }

        public bool CheckCollision(GameObject firstObject, GameObject secondObject)
        {
            if (!(_positionItems.ContainsKey(firstObject.Name)) && !(_positionItems.ContainsKey(firstObject.Name)))
            {
                return false;
            }
            QuadTreePositionItem<GameObject> originObject;
            QuadTreePositionItem<GameObject> targetObj;
            _positionItems.TryGetValue(firstObject.Name,out originObject);
            _positionItems.TryGetValue(secondObject.Name, out targetObj);
            
            if (originObject == null || targetObj == null)
            {
                return false;
            }
            
            QuadTreeNode<GameObject> originNode = CurrentTree.HeadNode.FindItemNode(originObject);
            if (originNode == null)
            {
                return false;
            }
            if (!originNode.Rect.Intersects(targetObj.Rect)) return false;
            bool returnBool = originObject.Rect.Intersects(targetObj.Rect);
            return returnBool;

        }

        public void ClearTree()
        {
            CurrentTree = new QuadTree<GameObject>(new Vector2(0f, 0f), 1);
            _positionItems.Clear();
        }
    }
}