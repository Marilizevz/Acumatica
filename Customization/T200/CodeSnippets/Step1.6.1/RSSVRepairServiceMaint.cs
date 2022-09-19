using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PhoneRepairShop
{
  public class RSSVRepairServiceMaint : PXGraph<RSSVRepairServiceMaint>
  {
////////// The added code
    protected void RSSVRepairService_WalkInService_FieldUpdated(
          PXCache cache, PXFieldUpdatedEventArgs e)
    {
      var row = (RSSVRepairService)e.Row;
      if (row.WalkInService == true)
      {
        row.PreliminaryCheck = false;
      }
      else
      {
        row.PreliminaryCheck = true;
      }
    }
////////// The end of added code

    public PXSave<RSSVRepairService> Save;
    public PXCancel<RSSVRepairService> Cancel;

    public SelectFrom<RSSVRepairService>.View RepairService;

    public PXFilter<MasterTable> MasterView;
    public PXFilter<DetailsTable> DetailsView;

    [Serializable]
    public class MasterTable : IBqlTable
    {

    }

    [Serializable]
    public class DetailsTable : IBqlTable
    {

    }
	
  }
}