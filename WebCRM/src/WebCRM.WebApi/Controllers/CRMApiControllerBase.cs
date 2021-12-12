namespace WebCRM.WebApi.Controllers
{
    using System;
    using System.Linq;
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    /// <summary>
    /// Base Api data controller for CRM functions
    /// </summary>
    /// <typeparam name="Model">ICRMDataModel</typeparam>
    /// <typeparam name="ViewModel">ICRMViewModel of Model</typeparam>
    /// <typeparam name="Repostitory">ICRMRepository for the Model and ViewModel</typeparam>
    /// <author>Daniel Lee Graf</author>
    public abstract class CRMApiControllerBase<Model, ViewModel, Repostitory>: ControllerBase
        where Model:ICRMDataModel<Model>
        where ViewModel:ICRMViewModel<Model>,new()
        where Repostitory: ICRMRepository<Model,ViewModel>
    {
        protected Repostitory _repo;
        protected IAppSecurityService _security;

        public CRMApiControllerBase(Repostitory repo, IAppSecurityService security)
        {
            this._repo = repo;
            this._security = security;
        }

        protected virtual Expression<Func<Model, bool>> RestrictedSelection()
        {
            //Allways false
            return n => 1 == 0;
        }

        protected virtual bool CanViewAll()
        {
            return this._security.IsAdmin || this._security.CanViewReports || this._security.CanAccountEnterData;
        }

        protected virtual bool CanCreate()
        {
            return this._security.IsAdmin || this._security.CanAccountEnterData;
        }

        protected virtual bool CanUpdate()
        {
            return this._security.IsAdmin || this._security.CanAccountEnterData;
        }

        protected virtual bool CanDelete()
        {
            return this._security.IsAdmin;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            if (this.CanViewAll())
            {
                var data = await this._repo.RetrieveAsync(n => 1 == 1);
                return Ok(data);
            }
            var restrictedData = await this._repo.RetrieveAsync(RestrictedSelection());
            return Ok(restrictedData);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (CanViewAll())
            {
                var (success, model) = await this._repo.RetrieveByIdAsync(id);
                if (success)
                {
                    return Ok(model);
                }
                return NotFound();
            }

            var viewableData = await this._repo.RetrieveAsync(RestrictedSelection());
            var selectedData = viewableData.Where(w => w.Id == id).FirstOrDefault();

            if (selectedData != null)
            {
                return Ok(selectedData);
            }

            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] ViewModel model)
        {
            if (model != null && CanCreate() && model.IsValid())
            {
                var (success, viewModel) = await this._repo.CreateAsync(model, this._security.UserID);
                if (success)
                {
                    return Ok(viewModel);
                }
                return NotFound(model);
            }
            if (model == null)
            {
                model = new ViewModel();
                model.ValidationErrorMessages.Add("Blank data posted, please check inputs");
            }
            if (!CanCreate())
            {
                model.ValidationErrorMessages.Add($"Not Authorized to create { typeof(Model).Name }");
            }
            return BadRequest(model);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] ViewModel model)
        {
            if (model != null && model.Id > 0 && CanUpdate() && model.IsValid())
            {
                var (success, viewModel) = await this._repo.UpdateAsync(model, this._security.UserID);
                if (success)
                {
                    return Ok(viewModel);
                }
                return NotFound(model);
            }
            if (model == null)
            {
                model = new ViewModel();
                model.ValidationErrorMessages.Add("Blank data posted, please check inputs");
            }
            if (!CanUpdate())
            {
                model.ValidationErrorMessages.Add($"Not Authorized to update { typeof(Model).Name }");
            }
            if ((model?.Id ?? 0) <=0)
            {
                model.ValidationErrorMessages.Add("Invalid Id");
            }
            return BadRequest(model);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (CanDelete() && id != default)
            {
                var success = await this._repo.DeleteAsync(id, this._security.UserID);
                if (success)
                {
                    return Ok();
                }
                return NotFound();
            }
            var model = new ViewModel();
            if (id <= 0)
            {
                model.ValidationErrorMessages.Add("Invalid Id, please check inputs");
            }
            if (!CanDelete())
            {
                model.ValidationErrorMessages.Add($"Not Authorized to delete { typeof(Model).Name }");
            }
            return BadRequest(model);
        }
    }
}
