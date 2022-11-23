using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGamePro
{
    class Program
    {
        /*
            Poker Game

            By Vathusan Vimalarajan

            This Program will asks the user if he wants to play or test the program
            If he choose play, he will be asked to enter the bet amount,then he will "handed" 5 cards and 
            he will asked if he wants to switch any of them (max 4 times). Then it say if he has a winning hand and display the payout amount and the new bank amount
            and finally he will be asked if he wants to play again.
            If he chooses the test option, the user will be offered to test one of the 9 Poker Hand methods.

            Program Tested by: Yorick Ntwari (Student)
         */
   
        #region Constants
        //Displays
        const string MENU_LAYOUT = @"
╔═══════════════════════════════════════════════════════════════╗
║                         Poker Tings                           ║
║                                                               ║
║                    by: Vathusan Vimalarajan                   ║
║                            a.k.a                              ║
║                         RED SLEEVES                           ║    
║                                                               ║
║                  Tested by: Yorick Ntwari (Student)           ║
╚═══════════════════════════════════════════════════════════════╝
1. Play
2. Test
3. Quit

Press 1, 2, or 3
";
        const string TEST_MENU = @"
╔═════════════════════════╗
║[1] Test Royal Flush     ║
║[2] Test Straight Flush  ║
║[3] Test Four of a Kind  ║
║[4] Test Full House      ║
║[5] Test Flush           ║
║[6] Test Straight        ║ 
║[7] Test Three of a kind ║
║[8] Test Two Pairs       ║
║[9] Test Pair            ║
║[0] QUIT                 ║
╚═════════════════════════╝
";
        //Limits
        const int HAND_SIZE = 5;
        const int MAX_TRADES = 4;
        const int TOTAL_FACES = 13;
        const int TOTAL_SUITS = 4;

        //Payouts
        const int PAY_ROYAL_FLUSH = 250;
        const int PAY_STRAIGHT_FLUSH = 50;
        const int PAY_FOUR_KIND = 25;
        const int PAY_FULL_HOUSE = 9;
        const int PAY_FLUSH = 6;
        const int PAY_STRAIGHT = 4;
        const int PAY_THREE_KIND = 3;
        const int PAY_TWO_PAIR = 2;
        const int PAY_PAIR = 1;
        const int LOSING_HAND = 0;
        #endregion 
        #region Main
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;


            bool playerQuit = false;
            do
            {
                //Choose if you want to play, test or quit
                Console.Clear();
                Console.Write(MENU_LAYOUT);
                switch(Console.ReadKey().Key)
                {
                    //Play
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Game(); 
                        break;
                    //Test
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Test();
                        break;
                    //Quit
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Console.WriteLine(MENU_LAYOUT);
                        Console.WriteLine("Are you sure you want to quit? press Y/N");
                        bool choiceValid;
                        do
                        {
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.Y:
                                    choiceValid = true;
                                    Environment.Exit(0);
                                    break;
                                case ConsoleKey.N:
                                    choiceValid = true;
                                    break;
                                default:
                                    choiceValid = false;
                                    break;
                            }
                        } while (!choiceValid);
                        break;

                }
            }while(!playerQuit); 
      



        }
        #endregion
        //This method does action while gameover value is false and deck is not empty
        #region PlayGame
        static void Game()
        {
            //Variables
            bool gameOver;
            int bankroll = 1000;//Amount of money in the bank
            do
            {
                Console.Clear();
                //Variables
                int cardCount = 0;
                Card[] Hand = new Card[HAND_SIZE];
                Deck deck = new Deck();
                deck.Shuffle();

                // Fills player hand
                for (int i = 0; i < HAND_SIZE; i++)
                {
                    Hand[i] = deck.DealACard(); 
                    cardCount++;
                }
                //Display Bank amount
                Console.WriteLine("Bankroll: ${0}\n", bankroll);
                //Ask for bet amount and displays the new bank amount
                Console.Write("How much would you like to bet? (1 to " + bankroll + ")\n$");
                int bet = InputValidation(1, bankroll);
                bankroll -= bet;
                Console.WriteLine("New Balance: $" + (bankroll) +"\n");

                //Call function (Sorts the given hand in order,Prints the hand and allows the user to switch cards)
                SortHand(Hand);
                PrintHand(Hand);
                ChooseCard(Hand, deck);
                int payout = bet * CheckHand(Hand); //Amount of money won
                bankroll += payout;//New Bank Amount with the payout
                //Displays Payout and New Bank Amount
                Console.WriteLine("Money won: ${0}", payout);
                Console.WriteLine("Money in the bank: ${0}", bankroll);
                //Ask for a new round
                if(bankroll > 0)
                {
                    Console.WriteLine("\nPress any key to continue or press \"N\" to quit");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            gameOver = true;
                            break;
                        default:
                            gameOver = false;
                            break;
                    }
                }
                else
                {
                    gameOver = true;
                }

                if (bankroll <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You lost all of your money!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to return to main menu");

                }

                Console.ReadKey();

            } while(bankroll > 0 && !gameOver);
            //When you lose all your money
           
        }
        #endregion
        //These methods help with the testing of each method that detects a different Poker Hand
        #region Test
        static void Test()
        {
            Console.Clear();
            Console.WriteLine("Automatic testing");
            //Variables
            int bet;
            int payout = 0;
            int bankroll = 1000;
            bool validChoice = false;

            //Runs Test
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine(TEST_MENU);
                switch (Console.ReadKey().Key)
                {
                    //Test Royal Flush
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1,bankroll);
                        Console.WriteLine("You won: ${0}", payout = RoyalFlushTest(bet));
                        validChoice = true;
                        break;
                    //Test Straight Flush
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = StraightFlushTest(bet));
                        validChoice = true;
                        break;
                    //Test Four of a Kind
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = FourKindTest(bet));
                        validChoice = true;
                        break;
                    //Test FullHouse
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = FullHouseTest(bet));
                        validChoice = true;
                        break;
                    //Test Flush
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = FlushTest(bet));
                        validChoice = true;
                        break;
                    //Test Straight
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = StraightTest(bet));
                        validChoice = true;
                        break;
                    //Test Three of a Kind
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = ThreeKindTest(bet));
                        validChoice = true;
                        break;
                    //Test Two Pair
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You won: ${0}", payout = TwoPairTest(bet));
                        validChoice = true;
                        break;
                    //Test Pair
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        Console.Clear();
                        Console.WriteLine(TEST_MENU);
                        Console.Write("How much would you like to bet?(1...1000)");
                        bet = InputValidation(1, bankroll);
                        Console.WriteLine("You Won ${0}", payout = PairTest(bet));
                        validChoice = true;
                        break;
                    //Quit
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        return;
                    default:
                        validChoice = false;
                        break;
                }
            } while (!validChoice);

            Console.WriteLine("New Balance: ${0}", bankroll += payout);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return to test menu");
            Console.ReadKey();
        }
        //Poker Hand Testing
        static int RoyalFlushTest(int bet)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
            testHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Jack);
            testHand[2] = new Card(Card.Suit.Spades, Card.FaceValue.Queen);
            testHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.King);
            testHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Ace);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_ROYAL_FLUSH * bet;
        }
        
        static int StraightFlushTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Hearts, Card.FaceValue.Five);
            testHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Six);
            testHand[2] = new Card(Card.Suit.Hearts, Card.FaceValue.Seven);
            testHand[3] = new Card(Card.Suit.Hearts, Card.FaceValue.Eight);
            testHand[4] = new Card(Card.Suit.Hearts, Card.FaceValue.Nine);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_STRAIGHT_FLUSH * bet;
        }

        static int FourKindTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Jack);
            testHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Jack);
            testHand[2] = new Card(Card.Suit.Clubs, Card.FaceValue.Jack);
            testHand[3] = new Card(Card.Suit.Diamonds, Card.FaceValue.Jack);
            testHand[4] = new Card(Card.Suit.Clubs, Card.FaceValue.Ace);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_FOUR_KIND * bet;
        }

        static int FullHouseTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Hearts, Card.FaceValue.Five);
            testHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Five);
            testHand[2] = new Card(Card.Suit.Diamonds, Card.FaceValue.Five);
            testHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Queen);
            testHand[4] = new Card(Card.Suit.Clubs, Card.FaceValue.Queen);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_FOUR_KIND * bet;
        }

        static int FlushTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Hearts, Card.FaceValue.Three);
            testHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Six);
            testHand[2] = new Card(Card.Suit.Hearts, Card.FaceValue.Eight);
            testHand[3] = new Card(Card.Suit.Hearts, Card.FaceValue.Jack);
            testHand[4] = new Card(Card.Suit.Hearts, Card.FaceValue.King);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_FLUSH * bet;
        }

        static int StraightTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Hearts, Card.FaceValue.Five);
            testHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Six);
            testHand[2] = new Card(Card.Suit.Clubs, Card.FaceValue.Seven);
            testHand[3] = new Card(Card.Suit.Hearts, Card.FaceValue.Eight);
            testHand[4] = new Card(Card.Suit.Diamonds, Card.FaceValue.Nine);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_STRAIGHT * bet;
        }

        static int ThreeKindTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Five);
            testHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Five);
            testHand[2] = new Card(Card.Suit.Clubs, Card.FaceValue.Five);
            testHand[3] = new Card(Card.Suit.Clubs, Card.FaceValue.Jack);
            testHand[4] = new Card(Card.Suit.Diamonds, Card.FaceValue.Queen);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_THREE_KIND * bet;
        }

        static int TwoPairTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Diamonds, Card.FaceValue.Four);
            testHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Four);
            testHand[2] = new Card(Card.Suit.Diamonds, Card.FaceValue.Seven);
            testHand[3] = new Card(Card.Suit.Clubs, Card.FaceValue.Ace);
            testHand[4] = new Card(Card.Suit.Diamonds, Card.FaceValue.Ace);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_TWO_PAIR * bet;
        }

        static int PairTest(int bet)
        {
            Console.Clear();
            Console.WriteLine(TEST_MENU);
            //Fixed Hand
            Card[] testHand = new Card[HAND_SIZE];
            testHand[0] = new Card(Card.Suit.Diamonds, Card.FaceValue.Two);
            testHand[1] = new Card(Card.Suit.Clubs, Card.FaceValue.Six);
            testHand[2] = new Card(Card.Suit.Hearts, Card.FaceValue.Queen);
            testHand[3] = new Card(Card.Suit.Diamonds, Card.FaceValue.Queen);
            testHand[4] = new Card(Card.Suit.Hearts, Card.FaceValue.King);

            SortHand(testHand);
            PrintHand(testHand);
            Console.ForegroundColor = ConsoleColor.Green;

            return PAY_PAIR * bet;
        }
        #endregion
        //These method help with creation, sorting , replacement and checking of the player's Hand
        #region Hand Functions
        //This method organized the hand in order of FaceValue
        static void SortHand(Card[] Hand)
        {
            //Variables
            bool swap;
            Card temp;

            do
            {
                swap = false;
                for (int i = 0; i < HAND_SIZE - 1; i++)
                {
                    //Sorts according to face value
                    if (Hand[i].GetFaceValue() > Hand[i + 1].GetFaceValue())
                    {
                        temp = Hand[i];
                        Hand[i] = Hand[i + 1];
                        Hand[i + 1] = temp;
                        swap = true;
                    }
                }
            } while (swap);
        }
        //Displays the Cards with the ToString method from Deck class
        static void PrintHand(Card[] Hand)
        {
            Console.WriteLine("Your Hand:\n");
            for (int i = 0; i < HAND_SIZE; i++)
            {
                Console.WriteLine("║ [{0}] ║ {1, -17} ║", i + 1, Hand[i].ToString());
            }
        }
        //This method allows the user to choose the card that he like to replace and replaces it by calling the ExchangeCard method(limits: 4 changes max and can't choose the same one )
        static void ChooseCard(Card[] Hand, Deck deck)
        {

            Console.WriteLine("Select up to 4 card you would like to discard and replace");
            //Variable
            int currChoice = 1;
            bool isDone = false;
            bool card1Picked = false;
            bool card2Picked = false;
            bool card3Picked = false;
            bool card4Picked = false;
            bool card5Picked = false;
            while (currChoice <= MAX_TRADES)
            {
                Console.Write("Choose the card you want to change (enter \"0\" to continue): ");
                int choice = InputValidation(0, 5);
                //Choose the card to replace
                switch (choice)
                {
                    case 0:
                        isDone = true;
                        break;
                    case 1:
                        if (card1Picked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Card was Already Picked");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Card successfully discarded");
                        Console.ForegroundColor = ConsoleColor.White;
                        card1Picked = true;
                        currChoice++;
                        ExchangeCard(Hand, choice - 1, deck);//Call the replacement function
                        isDone = false;
                        break;
                    case 2:
                        if (card2Picked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Card was Already Picked");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Card successfully discarded");
                        Console.ForegroundColor = ConsoleColor.White;
                        card2Picked = true;
                        currChoice++;
                        ExchangeCard(Hand, choice - 1, deck);//Call the replacement function
                        isDone = false;
                        break;
                    case 3:
                        if (card3Picked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Card was Already Picked");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Card successfully discarded");
                        Console.ForegroundColor = ConsoleColor.White;
                        card3Picked = true;
                        currChoice++;
                        ExchangeCard(Hand, choice - 1, deck);//Call the replacement function
                        isDone = false;
                        break;
                    case 4:
                        if (card4Picked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Card was Already Picked");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Card successfully discarded");
                        Console.ForegroundColor = ConsoleColor.White;
                        card4Picked = true;
                        currChoice++;
                        ExchangeCard(Hand, choice - 1, deck);//Call the replacement function
                        isDone = false;
                        break;
                    case 5:
                        if (card5Picked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Card was Already Picked");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Card successfully discarded");
                        Console.ForegroundColor = ConsoleColor.White;
                        card5Picked = true;
                        currChoice++;
                        ExchangeCard(Hand, choice - 1, deck);//Call the replacement function
                        isDone = false;
                        break;
                }

                if (isDone)
                    break;
            }
            SortHand(Hand);//Sort new hand
            PrintHand(Hand);//Displays new hand
        }
        //This method changes the chosen card and deals a new Card with DealACard method from Deck class 
        static void ExchangeCard(Card[] Hand, int cardPosition, Deck deck)
        {
            Hand[cardPosition] = deck.DealACard();
        }
        //Checks if player has any of the winning hands by checking from the highest Hand Ranking (Royal Flush) to the lowest (Pair) and display a winning message
        //If one of them is a winner it return the payout value of the hand
        //If not it displays a losing message
        static int CheckHand(Card[] Hand)
        {
            int[] faceArray = new int[TOTAL_FACES];
            int[] suitArray = new int[TOTAL_SUITS];

            for(int i = 0; i < HAND_SIZE; i++)
            {
                faceArray[(int)Hand[i].GetFaceValue()]++;
                suitArray[(int)Hand[i].GetSuit()]++;
            }
            if(RoyalFlush(faceArray, suitArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Royal Flush!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_ROYAL_FLUSH;
            }
            if(StraightFlush(faceArray, suitArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Straight Flush!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_STRAIGHT_FLUSH;
            }
            if (FourKind(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Four of a Kind!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_FOUR_KIND;
            }
            if (FullHouse(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a FullHouse!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_FULL_HOUSE;
            }
            if (Flush(suitArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Flush!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_FLUSH;
            }
            if (Straight(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Straight!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_STRAIGHT;
            }
            if (ThreeKind(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Three of a Kind!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_THREE_KIND;
            }
            if (TwoPair(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Two Pair!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_TWO_PAIR;
            }
            if (Pair(faceArray))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You got a Pair!");
                Console.ForegroundColor = ConsoleColor.White;
                return PAY_PAIR;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No winning hand!");
                Console.ForegroundColor = ConsoleColor.White;
                return LOSING_HAND;
            }
        }
        #endregion
        //This methods checks the requirement for each winning hand,
        #region Combinations
        //Royal Flush = All cards same suit + One of each high ranking card (10,Jack,Queen,King,Ace)
        static bool RoyalFlush(int[] faceArray, int[] suitArray)
        {
            int faceValueCounter = 0;
            bool sameSuit = false;
            for (int i = (int)Card.FaceValue.Ten; i <= (int)Card.FaceValue.Ace; i++)
            {
                //Checks if there is one of each of the highest card FaceValues
                if (faceArray[i] == 1)
                {
                    faceValueCounter++;
                }
            }
            //Checks if all five cards are from the same suit
            for(int i = (int)Card.Suit.Hearts; i <= (int)Card.Suit.Spades; i++)
            {
                if(suitArray[i] == HAND_SIZE)
                {
                    sameSuit = true;
                }
            }
            //Checks if both requirements are met
            if (faceValueCounter == HAND_SIZE && sameSuit)
            {
                return true;
            }
            return false;
        }
        //Straight Flush = five consecutive FaceValue index should contain a 1 + all five card must be from same suit
        static bool StraightFlush(int[] faceArray, int[] suitArray)
        {
            bool isStraight = false;
            bool sameSuit = false;
            int faceCounter = 0;
            for(int i = (int)Card.FaceValue.Two; i < (int)Card.FaceValue.Ace; i++)
            {
                //Check if the index contains a 1, if so adds +1 to the counter
                if(faceArray[i] == 1)
                {
                    faceCounter++;
                }
                //if it's not a one restart counter (Shows that it's not consecutive)
                else
                {
                    faceCounter = 0;
                }
                //But if it's consecutive it sets as true
                if (faceCounter == 5)
                {
                    isStraight = true;
                }
            }
            //Checks if all five cards are from the same suit
            for (int i = (int)Card.Suit.Hearts; i <= (int)Card.Suit.Spades; i++)
            {
                if (suitArray[i] == HAND_SIZE)
                {
                    sameSuit = true;
                }
            }
            //Checks requirements
            if(isStraight && sameSuit)
            {
                return true;
            }
            return false;
        }
        //Four of a Kind = one of the index must have a value of 4
        static bool FourKind(int[] faceArray)
        {
            for(int i = (int)Card.FaceValue.Two; i <= (int)Card.FaceValue.Ace; i++)
            {
                if(faceArray[i] == 4)
                {
                    return true;
                }
            }
            return false;
        }
        //Full house = Three of a kind + Pair
        static bool FullHouse(int[] faceArray)
        {
            bool threeKindValid = false;
            bool pairValid = false;

            for (int i = (int)Card.FaceValue.Two; i <= (int)Card.FaceValue.Ace; i++)
            {
                //Checks if one of the FaceValue index contains a 3
                if (faceArray[i] == 3)
                {
                    threeKindValid = true;
                }
                //Checks if one of the FaceValue contains a 2
                if(faceArray[i] == 2)
                {
                    pairValid = true;
                }
            }
            //Checks for the requirements
            if (threeKindValid && pairValid)
            {
                return true;
            }
            return false;
        }
        //Flush = One of the Suit index must contain a 5
        static bool Flush(int[] suitArray)
        {
            for (int i = (int)Card.Suit.Hearts; i <= (int)Card.Suit.Spades; i++)
            {
                if (suitArray[i] == HAND_SIZE)
                {
                    return true;
                }
            }
            return false;
        }
        //Straight = five consecutive FaceValue index should contain a 1 
        static bool Straight(int[] faceArray)
        {
            int faceCounter = 0;
            for (int i = (int)Card.FaceValue.Two; i < (int)Card.FaceValue.Ace; i++)
            {
                if (faceArray[i] == 1)
                {
                    faceCounter++;
                }
                else
                {
                    faceCounter = 0;
                }
                if (faceCounter == 5)
                {
                    return true;
                }
            }          
            return false;
        }
        //Three of a kind = one of the FaceValue indexes must contain a 3
        static bool ThreeKind(int[] faceArray)
        {
            for (int i = (int)Card.FaceValue.Two; i <= (int)Card.FaceValue.Ace; i++)
            {
                if (faceArray[i] == 3)
                {
                    return true;
                }
            }
            return false;
        }
        //Two Pair = two FaceValue indexes must contain a 2
        static bool TwoPair(int[] faceArray)
        {
            int twoPairCounter = 0; 
            for(int i = (int)Card.FaceValue.Two; i <= (int)Card.FaceValue.Ace; i++)
            {
                if(faceArray[i] == 2)
                {
                    twoPairCounter++;
                }
                if (twoPairCounter == 2)
                {
                    return true;
                }
            }
            return false;
        }
        //Pair = One of the high cards (past Jack) must contain a 2
        static bool Pair(int[] faceArray)
        {
            for(int i = (int)Card.FaceValue.Jack; i <= (int)Card.FaceValue.Ace; i++)
            {
                if(faceArray[i] == 2)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        //Validates the input of the user , if not valid display message
        #region Input Validation
        static int InputValidation(int min, int max)
        {
            int choice;
            bool valid;
            do
            {
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (!valid || choice < min || choice > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!valid || choice < min || choice > max);

            return choice;
        }

        #endregion
    }
}
