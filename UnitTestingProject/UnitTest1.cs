using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace UnitTestingProject
{
    public class Tests
    {
        /// <summary>
        /// For this test I am using the classes Player and Tilemap.
        /// To check the player position, I will use the variables: tilemap_posX and tilemap_posY
        /// To generate a tilemap, I will use the variable mString. And then the method GenerateATestMap and ConvertToMap
        /// In player I will be using the methods: Move(int x, int y) and checkingForCollision(Tilemap _tilemap, char _char, Actor _actor, int x, int y) 
        /// 
        /// A player is created in a position of (3, 3). At the sametime, a map is created where the player can move. The map contains wall in its border 
        /// which the player cannot cross. In addition, there's two specific blocks in the position (4,5) and (5,5), the player should not be able to cross or be
        /// in the same position than these two blocks.
        /// </summary>



        public Player myPlayer = new Player(10, 1, 3, 3, 3, 3);

        // Creating the Tilemap 
        private Tilemap tileMap = new Tilemap();
        private string mString;

        [SetUp]
       
        public void Setup()
        {          
            mString = tileMap.GenerateATestMap(25, 10);
            tileMap.ConvertToMap(mString, tileMap.multidimensionalMap);
        }

        [Test]
        public void Test1()
        {
            
            ///<summary> 
            /// Attempt to move the player in different directions: Up, Right, Left and Down
            /// </summary>
            /// <param> 
            /// The vector of movement.  Movement(1,0) = Down, Movement(0, -1) = Left, Movement(-1, 0) = Up, Movement(0, 1) = Right
            /// </param>
            /// <return> 1) True if succesfully move Down 1 position.  
            /// 2) True if succesfully move Left 1 position 
            /// 3) True if succesfully moves Up 1 position 
            /// 4) True if succesfully move Right 1 position 
            /// In the end, the character return in the initial position (3,3) </return>
            /// 


            myPlayer.Movement(1, 0);
            Assert.AreEqual(4, myPlayer.tilemap_PosX);

            myPlayer.Movement(0, -1);
            Assert.AreEqual(2, myPlayer.tilemap_PosY);

            myPlayer.Movement(-1, 0);
            Assert.AreEqual(3, myPlayer.tilemap_PosX); 

            myPlayer.Movement(0, 1);
            Assert.AreEqual(3, myPlayer.tilemap_PosY);


            ///<summary> 
            ///The player is going to move, but this time is going to check if is colliding with a wall or a block. 
            ///The player will try to pass the borders
            ///</summary>
            ///<param>
            ///The tilemap is going to be consider, this is where the information of map is contained. 
            /// For the collision, the infomation needed are: A tilemap, a char, an actor (player), int x and int y. 
            ///</param>
            ///<returns>
            ///1) True. The player will try to move 1 position up, the map does not contain any block nor wall in that position so the player should be able to go there
            ///   it should return true, since the position of the player should be (3, 2)
            ///2) True. The player will move left five times. Since the wall is in 0 position in x, no matter how much the player tries to moves left, 
            ///it would never pass that position. 
            ///   it should return TRUE if the player position in x is NOT EQUAL to -2; if player moves like it was moving before it suppose to be in X = -2
            ///   but because of the wall cannot be -2. The result should be in 1 since the wall is in the position 0. 
            ///
            /// 
            /// </returns>


            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, -1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, -1))
            {
                //Cannot go there
            }
            else 
            {
                myPlayer.Movement(0, -1); 
            }

            // Check if player's position is (3,2)
            Assert.AreEqual(3, myPlayer.tilemap_PosX);
            Assert.AreEqual(2, myPlayer.tilemap_PosY);
            

            // The player moves left one position five times.
            for(int i = 0; i < 5; i++) 
            {
                if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, -1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, -1, 0))
                {
                    //Cannot go there
                }
                else
                {
                    myPlayer.Movement(-2, 0);
                }
            }

            Assert.AreNotEqual(-2, myPlayer.tilemap_PosX);  // -2 is where the player should be in x if it moves like before, but because of the wall this should be 1. this should be true.
            Assert.AreNotEqual(0, myPlayer.tilemap_PosX); // The wall is in position 0 in x, no matter how much the player moves cannot reach this position. This should be true.
            Assert.AreEqual(1, myPlayer.tilemap_PosX); // The player should be in 1 in x, so this should be true.



            ///<summary> 
            ///This is the same as the previous test but with the wall that are in y = 0. This time, the player is going to be just the neccesary to be in that position
            /// </summary>
            /// <returns> 
            /// The player won't reach y = 0 because it will colliding with the wall. 
            /// 1) True, because the player is not going to be in 0 position in Y
            /// 2) True, because the player will stop going up when reaches the position 1 in Y
            /// 3) True, because the player has not moved horizontally in this test
            /// </returns>




            for (int i = 0; i < 2; i++)
            {
                if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, -1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, -1))
                {
                    //Cannot go there
                }
                else
                {
                    myPlayer.Movement(0, -1);
                }
            }

            Assert.AreNotEqual(0, myPlayer.tilemap_PosY); // The player should be in the position 0 in y if it would move normally but since is colliding with a wall
                                                          // is preventing to reach the position 0 in y, is this should be true

            // Test if the player is in the position (1,1) 
            Assert.AreEqual(1, myPlayer.tilemap_PosY);
            Assert.AreEqual(1, myPlayer.tilemap_PosX); 




            ///<summary>
            /// The player will try to pass the blocks that are located in the position (5,4) and (5,5)
            /// But first, lets move the player in the position (4,4) and then start with the test
            /// </summary>

            // This is to move the player to the position (4,4)
            for(int i = 0; i < 3; i++) 
            {
                if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 1, 0 ) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 1, 0))
                {
                    //Cannot go there
                }
                else
                {
                    myPlayer.Movement(1, 0);
                }

                if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, 1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, 1))
                {
                    ///Cannot go there
                }
                else
                {
                    myPlayer.Movement(0, 1);
                }
            }

            // Test if the player is actually in the position (4,4)
            Assert.AreEqual(4, myPlayer.tilemap_PosY);
            Assert.AreEqual(4, myPlayer.tilemap_PosX);



            /// <summary>
            /// The player will move right, so it will try to be in the position (5,4), but because there is a block in that position
            /// the player will remain in the position (4,4)
            /// </summary>
            /// <returns>
            /// 1) True, is colliding with a Block.
            /// 2) True, because the player couldn't move 1 position to the right because of the block, so the position in x shouldn't be 5
            /// 
            /// </returns>

            bool collidingWithBlock = myPlayer.checkingForCollision(tileMap, '$', myPlayer, 1, 0);  //Its true if the player collides with the block after trying to move right

            Assert.isTrue(collidingWithBlock);


            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 1, 0))
            {
                //Cannot go there
            }
            else
            {
                myPlayer.Movement(1, 0);
            }

            Assert.AreNotEqual(5, myPlayer.tilemap_PosX);



            ///<summary>
            ///The player will first move 1 position down, to be in the position (4,5). This should be possible since there is no block in that position
            ///And then, the player will try to move 1 position to the right where a block exits. The player should not be able to go in that position (5,5)
            /// </summary>
            /// <returns>
            /// 1) Return true because the player first move 1 position down, since there was nothing the prevent that movement, the player position in y should be 5
            /// 2) Return true  because the player couldn't move 1 position to the right because of the block, so the position in x shouldn't be 5
            /// 
            /// </returns>



            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 0, 1) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 0, 1))
            {
                //Cannot go there
            }
            else
            {
                myPlayer.Movement(0, 1);
            }

            if (myPlayer.checkingForCollision(tileMap, '#', myPlayer, 1, 0) || myPlayer.checkingForCollision(tileMap, '$', myPlayer, 1, 0))
            {
                //Cannot go there
            }
            else
            {
                myPlayer.Movement(1, 0);
            }

            Assert.AreEqual(5, myPlayer.tilemap_PosY);
            Assert.AreNotEqual(5, myPlayer.tilemap_PosX);
            Assert.AreEqual(4, myPlayer.tilemap_PosX); 

        }
    }
}