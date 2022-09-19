using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PhoneRepairShop
{
  public class RSSVRepairServiceMaint : PXGraph<RSSVRepairServiceMaint>
  {
////////// The added code    
    protected void _(Events.FieldUpdated<RSSVRepairService, 
        RSSVRepairService.walkInService> e)
    {
      
      var row = e.Row;
////////// The end of added code
      if (row.WalkInService == true)
        {
          row.PreliminaryCheck = false;
        }
        else
        {
          row.PreliminaryCheck = true;
        }
      
    }

    protected void _(Events.FieldUpdated<RSSVRepairService,
             RSSVRepairService.preliminaryCheck> e)
        {
            var row = e.Row;
            if (row.PreliminaryCheck == true)
            {
                row.WalkInService = false;
            }
            else
            {
                row.WalkInService = true;
            }
        }

		public PXSave<RSSVRepairService> Save;
		public PXCancel<RSSVRepairService> Cancel;
		
		public SelectFrom<RSSVRepairService>.View RepairService;
  }
}