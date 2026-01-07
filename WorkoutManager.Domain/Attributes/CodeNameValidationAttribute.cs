using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WorkoutManager.Domain.Attributes;

/// <summary>
/// Egyedi validációs attribútum az edzésprogram kódnevéhez.
/// Szabályok:
/// - Hossz: 6-12 karakter
/// - Csak nagybetűk (A-Z) és számjegyek (0-9)
/// - Nem kezdődhet számmal
/// - Nem tartalmazhat szóközt
/// - A speciális karakterek közül csak alulvonás (_) és felkiáltójel (!) engedélyezett
/// </summary>
public class CodeNameValidationAttribute : ValidationAttribute
{
    private const int MinLength = 6;
    private const int MaxLength = 12;
    private const string Pattern = @"^[A-Z_!][A-Z0-9_!]*$";

    public CodeNameValidationAttribute()
    {
        ErrorMessage = "A kódnév 6-12 karakter hosszú lehet, csak nagybetűket, számokat, alulvonást (_) vagy felkiáltójelet (!) tartalmazhat, és nem kezdődhet számmal.";
    }

    /// <summary>
    /// Ellenőrzi, hogy az érték megfelel-e a validációs szabályoknak.
    /// </summary>
    /// <param name="value">A validálandó érték</param>
    /// <param name="validationContext">A validációs kontextus</param>
    /// <returns>ValidationResult.Success ha helyes, egyébként hibaüzenet</returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("A kódnév megadása kötelező.");
        }

        string codeName = value.ToString()!;

        // Hossz ellenőrzése
        if (codeName.Length < MinLength || codeName.Length > MaxLength)
        {
            return new ValidationResult($"A kódnév hossza {MinLength} és {MaxLength} karakter között kell legyen.");
        }

        // Szóköz ellenőrzése
        if (codeName.Contains(' '))
        {
            return new ValidationResult("A kódnév nem tartalmazhat szóközt.");
        }

        // Ellenőrizzük, hogy nem kezdődik-e számmal
        if (char.IsDigit(codeName[0]))
        {
            return new ValidationResult("A kódnév nem kezdődhet számmal.");
        }

        // Minta ellenőrzése: csak nagybetűk, számok, alulvonás és felkiáltójel
        if (!Regex.IsMatch(codeName, Pattern))
        {
            return new ValidationResult("A kódnév csak nagybetűket (A-Z), számokat (0-9), alulvonást (_) és felkiáltójelet (!) tartalmazhat.");
        }

        return ValidationResult.Success;
    }

    /// <summary>
    /// Egyszerű validáció bool visszatérési értékkel.
    /// </summary>
    /// <param name="value">A validálandó érték</param>
    /// <returns>True ha érvényes, egyébként false</returns>
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return false;
        }

        string codeName = value.ToString()!;

        // Minden szabály ellenőrzése
        if (codeName.Length < MinLength || codeName.Length > MaxLength)
            return false;

        if (codeName.Contains(' '))
            return false;

        if (char.IsDigit(codeName[0]))
            return false;

        if (!Regex.IsMatch(codeName, Pattern))
            return false;

        return true;
    }
}
