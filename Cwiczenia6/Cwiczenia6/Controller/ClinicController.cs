using Cwiczenia6.Models;
using Cwiczenia6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia6.Controller;

[Route("api/prescription")]
[Controller]
public class ClinicController : ControllerBase
{

    private PatientRepository _patientRepository = new PatientRepository();
    private MedicamentRepository _medicamentRepository = new MedicamentRepository();
    private PrescriptionRepository _prescriptionRepository = new PrescriptionRepository();
    
    [HttpPost]
    public IActionResult AddRecipe(RecipeRequest request)
    {
        if (!_patientRepository.PatientExists(request.Patient.IdPatient))
        {
            _patientRepository.createPatient(request.Patient);
        }

        if (request.Medicaments.Count > 10)
            return BadRequest("Too many medicaments");
        
        if (request.Date < request.DueDate)
            return BadRequest("Invalid due date");
        
        if (request.Medicaments.Any(m => !_medicamentRepository.MedicamentExists(m)))
        {
            throw new ArgumentException("Medicament doesn't exists");
        }
        
        return Ok();
    }

    [HttpGet("{patientId}")]
    public IActionResult GetPatientPrescriptions(int patientId)
    {
        GetPatientPrescriptionsResponse response = new GetPatientPrescriptionsResponse();
        Patient patient = _patientRepository.getPatient(patientId);
        response.IdPatient = patient.IdPatient;

        ICollection<Prescription> prescriptions =
            _prescriptionRepository.getPrescriptionsForPatient(patientId);

        response.Prescriptions = prescriptions;
        
        return Ok(response);
    }
}