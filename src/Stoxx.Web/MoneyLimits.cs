namespace Stoxx.Web;

public static class MoneyLimits
{
    public const decimal MaximumAmount = 9_999_999_999.99m;

    public static bool IsValid(decimal amount) =>
        amount >= 0 && amount <= MaximumAmount && decimal.Round(amount, 2) == amount;
}
