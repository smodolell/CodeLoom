namespace CodeLoom.Business.Financial.Data;

[Entity("Credits")]
[Page("Créditos", "Sistema de gestión de créditos financieros", "Financial")]
public class Credit
{
    [Key]
    public Guid Id { get; set; }

    [Page("ID de Frecuencia", "Identificador de la frecuencia de pago", "Relationships")]
    public int FrequencyId { get; set; }

    [Required]
    [MaxLength(30)]
    [Page("Clave de Crédito", "Identificador único del crédito", "Basic")]
    public string CreditKey { get; set; } = "";

    [Page("Tasa de Interés", "Tasa de interés aplicada al crédito", "Financial")]
    public decimal Rate { get; set; }

    [Page("Tasa de Impuesto", "Tasa de impuesto aplicada", "Financial")]
    public decimal TaxRate { get; set; }

    [Page("Plazo", "Duración del crédito en periodos", "Financial")]
    public int Term { get; set; }

    [Page("Monto Principal", "Monto principal del crédito", "Financial")]
    public decimal PrincipalAmount { get; set; }

    [Page("Monto Financiado", "Monto total financiado", "Financial")]
    public decimal FinancedAmount { get; set; }

    [Page("Estado del Crédito", "Estado actual del crédito", "Status")]
    public CreditState CreditState { get; set; }

    [ManyToOne(nameof(FrequencyId))]
    [Page("Frecuencia", "Frecuencia de pagos del crédito", "Relationships")]
    public Frequency Frequency { get; set; } = null!;
}

[Entity("Frequencies")]
[Page("Frecuencias", "Catálogo de frecuencias de pago", "Catalog")]
public class Frequency
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [Page("Nombre", "Nombre de la frecuencia", "Basic")]
    public string Name { get; set; } = "";

    [OneToMany(nameof(Credit.Frequency))]
    [Page("Créditos", "Créditos asociados a esta frecuencia", "Relationships")]
    public ICollection<Credit> Credits { get; set; } = new List<Credit>();
}

[Page("Estados de Crédito", "Estados posibles de un crédito financiero", "Enums")]
public enum CreditState
{
    Captured = 1,
    Activated = 2,
    Canceled = 3,
    Finished = 4
}