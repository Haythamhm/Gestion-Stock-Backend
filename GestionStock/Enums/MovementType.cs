namespace GestionStock.Enums
{
    public enum MovementType
    {
        In = 1,         // Réception, retour client, etc.
        Out = 2,        // Vente, casse, perte, etc.
        Transfer = 3,   // Transfert entre entrepôts
        Adjustment = 4  // Ajustement inventaire
    }
}

