// DEFINICIÓN DE CARTAS QUE TIENEN INERACCIÓN SI ESTÁN DELANTE // 

// Si el player es foca y va a la casilla suma el value a la vida cuando cae encima || si el player está delante desaparecen
public class FishPrize
{
    private int fValue = 3;

    public int Value
    {
        get { return fValue; }
        set {fValue = value; }
    }

    public FishPrize(int v)
    {
    }
}

// Si está delante o cae en la misma casilla -> si el player es humano: no puedes girar en el siguiente turno, foca: resta el value a la vida
public class Hunter
{
    private int value = 2;

    public int Value
    {
        get { return value; }
    }

    public Hunter()
    {
    }
}

// Si el player está delante de esta carta o cae en la misma casilla -> si es foca -> resta sealValue a la vida, si es humano resta humanValue a la vida
public class Whale
{
    private int value = 5;

    public int Value
    {
        get { return value; }
    }
    public Whale()
    {}
}

// Si el player está delante de esta carta o cae en la misma casilla -> si es humano resta humanValue a las monedas. Si es foca hay que restarle attackValue a la vida del player y sumar coinValue a las monedas
/*public class Pirate
{
    private int coinValue = 3;
    private int attackValue = 3;
    private int humanValue = 2;

    public int SealValue
    {
        get { return coinValue; }
    }
    public int HumanValue
    {
        get { return humanValue; }
    }

    public int AttackValue
    {
        get { return attackValue; }
    }
    public Pirate()
    {}
}*/