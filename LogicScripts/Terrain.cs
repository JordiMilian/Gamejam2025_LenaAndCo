// DEFINICIÓN DE CARTAS QUE NO TIENEN INERACCIÓN SI ESTÁN DELANTE // 

// Si el player es humano suma el value a las monedas del player
public class CoinPrize
{
    private int cValue = 2;

    public int Value
    {
        get { return cValue; }
        set { cValue = value; }
    }

    public CoinPrize()
    {
    }
}

// Si el player humano resta monedas
public class Seagull // NO PRIORITARI
{
    private int value = 3;

    public int Value
    {
        get { return value; }
    }

    public Seagull()
    {
    }
}

// Si el player es foca resta vida
public class SeaUrchin // NO PRIORITARI
{
    private int value = 4;

    public int Value
    {
        get { return value; }
    }

    public SeaUrchin()
    {
    }
}

// Iceberg -> resta -1 a las CoinPrize y FishPrice de la fila siguiente al player
public class Iceberg
{    public Iceberg()
    {    }
}

// Barco x2 siguente enemigo que te ataque si foca
public class Ship 
{    public Ship()
    {    }
}

// Red te obliga a no girar en el siguiente turno solo 
public class Net 
{    public Net()
    {    }
}

// Red te obliga a no girar en el siguiente turno solo 
public class FinalBoss 
{    
    int attackValue = 5;

    public int Value
    {
        get { return attackValue; }
    }
}