using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RPG_Elfshock.Common.CharacterConstants;

namespace Models
{
    public class Character : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public string ClassName { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CharacterCreationDate { get; set; }

        [Required]
        public char Symbol { get; set; }

        [Required]
        [Range(0, 255)]
        public int Strength { get; set; }

        [Required]
        [Range(0, 255)]
        public int Agility { get; set; }

        [Required]
        [Range(0, 255)]
        public int Intelligence { get; set; }

        [Required]
        [Range(0, 255)]
        public int Range { get; set; }

        [Required]
        [Range(0, 255)]
        public int Health { get; set; }

        [Required]
        [Range(0, 255)]
        public int Mana { get; set; }

        [Required]
        [Range(0, 255)]
        public int Damage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!characterClassesNames.Contains(ClassName))
            {
                yield return new ValidationResult(classInvalidMsg);
            }

            if (!pcEntitySymbol.Contains(Symbol))
            {
                yield return new ValidationResult(symbolInvalidMsg);
            }
        }
    }
}