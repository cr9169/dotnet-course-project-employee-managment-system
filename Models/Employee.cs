using System.ComponentModel.DataAnnotations;

public class Employee {

    // The Entity Framework created an id ny itself and we check if ok in the business layer, so we don't need to add "Required".
    public int Id {get; set;}
    
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName {get; set;}

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName {get; set;}

    [Required(ErrorMessage = "Email is required")]
    // Validate if not a valid email adress.
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    // Validate Length range.
    [Length(5, 30)]
    public string Email {get; set;}

    [Required(ErrorMessage = "Phone Number is required")]
    [Length(9,10)]
    public string PhoneNumber {get; set;}

    [Required(ErrorMessage = "Position is required")]
    public string Position {get; set;}
}