public class Card
{
	/// <summary>
	/// A Card has two fields: the Face Value field and the Suit field.
	/// This Class contains 2 constructors (the default and second).
	/// As well, the 2 Getters for each field. 
	/// There is also the ToString override which will display the value and suit of the card. 
	/// </summary>
    public enum FaceValue { Two = 0, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
    // Note: Ace is both the lowest and highest card
    public enum Suit { Hearts, Diamonds, Clubs, Spades };

    private FaceValue _faceValue;
	private Suit _suit;

	//Constructors
	//Default Constructor
	public Card()
	{
		_suit = Suit.Spades;
		_faceValue = FaceValue.Ace;
	}
	//Second Constructor
	public Card(Suit theSuit_, FaceValue theFaceValue_)
	{
		_suit = theSuit_;
		_faceValue = theFaceValue_;
	}
	//Display the specific Card Values
	public override string ToString()
	{
		return _faceValue + " of " + _suit;
	}
	//Getter Suits
	public Suit GetSuit()
	{
		return _suit;
	}
	//Getter FaceValue
	public FaceValue GetFaceValue()
	{
		return _faceValue;
	}
}
