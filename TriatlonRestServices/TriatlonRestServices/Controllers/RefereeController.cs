using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Repository.Repository;
using System.Collections.Generic;

namespace TriatlonRestServices.Controllers
{
    [ApiController]
    [Route("/api/referee")]
    public class RefereeController : ControllerBase
    {
        private readonly RefereeRepository _refereeRepository;
        private readonly TrialRepository _trialRepository;

        public RefereeController(RefereeRepository refereeRepository, TrialRepository trialRepository)
        {
            _refereeRepository = refereeRepository;
            _trialRepository = trialRepository;
        }

        [HttpGet]
        public ActionResult<List<Referee>> GetAll()
        {
            return Ok(_refereeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Referee> GetById(long id)
        {
            var referee = _refereeRepository.GetById(id);
            if (referee == null)
            {
                return NotFound();
            }
            return Ok(referee);
        }

        [HttpPost]
        public ActionResult<Referee> Save([FromBody] RefereeCreateDTO refereeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trial = _trialRepository.GetById(refereeDTO.trial_id);
            if (trial == null)
            {
                return NotFound($"Trial with id {refereeDTO.trial_id} not found.");
            }

            var referee = new Referee
            {
                name = refereeDTO.Name,
                password = refereeDTO.Password,
                trial = trial,
                TrialId = refereeDTO.trial_id
            };

            var savedReferee = _refereeRepository.Save(referee);
            return CreatedAtAction(nameof(GetById), new { id = savedReferee.id }, savedReferee);
        }

        [HttpPut("{id}")]
        public ActionResult<Referee> Update(long id, [FromBody] RefereeUpdateDTO refereeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReferee = _refereeRepository.GetById(id);
            if (existingReferee == null)
            {
                return NotFound();
            }

            var trial = _trialRepository.GetById(refereeDTO.trial_id);
            if (trial == null)
            {
                return NotFound($"Trial with id {refereeDTO.trial_id} not found.");
            }

            existingReferee.name = refereeDTO.Name;
            existingReferee.password = refereeDTO.Password;
            existingReferee.trial = trial;
            existingReferee.TrialId = refereeDTO.trial_id;

            var updatedReferee = _refereeRepository.Update(existingReferee);
            return Ok(updatedReferee);
        }

        [HttpDelete("{id}")]
        public ActionResult<Referee> DeleteById(long id)
        {
            var deletedReferee = _refereeRepository.DeleteById(id);
            if (deletedReferee == null)
            {
                return NotFound();
            }
            return Ok(deletedReferee);
        }
    }
}
