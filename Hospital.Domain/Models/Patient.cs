using Hospital.Domain.Events;
using Hospital.Domain.Infastructure;
using System.Diagnostics.CodeAnalysis;

namespace Hospital.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public string Name { get; set; }

        [AllowNull]
        public string? Breed { get; set; }

        public int Age { get; set; }

        [AllowNull]
        public int? Weight { get; set; }

        [AllowNull]
        public string? BloodType { get; set; }

        public PatientStatus PatientStatus { get; set; }

        public void ApplyStateFromEvent(IEvent patientEvent)
        {
            switch (patientEvent)
            {
                case PetAdmittedEvent:
                    PatientStatus = PatientStatus.Admitted;
                    break;
                case PetDischargedEvent:
                    PatientStatus = PatientStatus.Discharged;
                    break;
                case PetTransferedEvent e:
                    PetId = e.PetId;
                    Name = e.Name;
                    Breed = e.Breed;
                    Age = e.Age;
                    break;
                case SetPatiantBloodTypeEvent e:
                    BloodType = e.BloodType;
                    break;
                case SetPatiantWeightEvent e:
                    Weight = e.Weight;
                    break;
                default:
                    break;
            }
        }
    }
}
