using System;
using System.Collections.Generic;
using DbCore.DbRegjistrim;

namespace DbCore.classes.ProcessedOrder
{
    public class ProcessOrder
    {
        public string supplier { get; set; }
        public List<Item> items { get; set; }

        public Metadata metadata { get; set; }

        public bool processed { get; set; }

        public string type { get; set; }
        public ProcessOrderStatus status { get; set; }
        public ProcessOrder(string supplier, List<Item> items, Metadata metadata, bool processed, string type, ProcessOrderStatus status)
        {
            this.supplier = supplier;
            this.items = items;
            this.metadata = metadata;
            this.processed = processed;
            this.type = type;
            this.status = status;
        }

        public static ProcessOrder fromInvoice(string supplier, colTrupiShitje invoiceBody, string uid, string organization, ProcessEnums type, int enterpriseId)
        {
            Metadata metadata = new Metadata(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), uid, "", "", organization, "");
            List<Item> items = new List<Item>();
            for (int i = 0; i < invoiceBody.Count; i++)
            {
                Item item = Item.fromClsTrupiShitje(invoiceBody[i], enterpriseId);
                items.Add(item);
            }
            ProcessOrder order = new ProcessOrder(supplier, items, metadata, false, type.ToString(), ProcessOrderStatus.pending);
            return order;
        }
        public object toObject()
        {
            object[] items = new object[this.items.Count];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = this.items[i].toObject();
            }
            return new
            {
                supplier = this.supplier,
                items = items,
                metadata = this.metadata.toObject(),
                processed = this.processed,
                type = this.type
            };
        }
    }
}
