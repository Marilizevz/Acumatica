﻿using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
public class SOInvoiceOrder_Workflow : PXGraphExtension<SOInvoiceEntry_Workflow, SOInvoiceEntry>
{
    public static class ActionCategories
    {
        public const string RepairCategoryID = "Repair Orders Category";

        [PXLocalizable]
        public static class DisplayNames
        {
            public const string RepairOrders = "Repair Orders";
        }
    }

    public override void Configure(PXScreenConfiguration config)
    {
        Configure(config.GetScreenConfigurationContext<SOInvoiceEntry, ARInvoice>());
    }

    protected virtual void Configure(WorkflowContext<SOInvoiceEntry, ARInvoice> context)
    {
        var repairCategory = context.Categories.CreateNew(ActionCategories.RepairCategoryID,
            category => category.DisplayName(ActionCategories.DisplayNames.RepairOrders));

        var viewOrder = context.ActionDefinitions
          .CreateExisting<SOInvoiceEntry_Extension>(g => g.ViewOrder,
            a => a.WithCategory(repairCategory));

        context.UpdateScreenConfigurationFor(screen => screen
            .UpdateDefaultFlow(flow =>
            {
                return flow
                    .WithFlowStates(flowStates =>
                    {
                        flowStates.Update<ARDocStatus.open>(flowState =>
                        {
                            return flowState.WithActions(actions =>
                                actions.Add(viewOrder));
                        });
                    });
            })
            .WithCategories(categories =>
            {
                categories.Add(repairCategory);
            })
            .WithActions(actions =>
            {
                actions.Add(viewOrder);
            })
);
    }
}