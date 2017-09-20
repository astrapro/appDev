using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{

    public class ProgressItem
    {
        public int SerialNo { get; set; }
        public string Work_Title { get; set; }
        public eProgressStatus Status { get; set; }

        public ProgressItem()
        {
            SerialNo = -1;
            Work_Title = "";
            Status = eProgressStatus.Waiting;
        }
    }
    public class ProgressList : List<ProgressItem>
    {
        public ProgressList()
            : base()
        {

        }
        public ProgressList(List<string> work_list)
            : base()
        {
            ProgressItem pitm = null;
            int _cnt = 0;
            foreach (var item in work_list)
            {
                pitm = new ProgressItem();
                pitm.SerialNo = ++_cnt;
                pitm.Work_Title = item;
                pitm.Status =  eProgressStatus.Waiting;

                Add(pitm);
            }

            CurrentIndex = -1;
        }

        public int CurrentIndex { get; set; }
        public void Next()
        {
            try
            {
                if (this.Count == 0) return;

                if (CurrentIndex > -1)
                {
                    this[CurrentIndex].Status = eProgressStatus.Done;
                }

                CurrentIndex = CurrentIndex + 1;
                if (CurrentIndex == Count && Count != 0)
                    CurrentIndex = Count - 1;
            }
            catch (Exception ex) { }
        }
        public void Add(string work)
        {
            ProgressItem pi = new ProgressItem();
            pi.SerialNo = Count + 1;
            pi.Work_Title = work;
            pi.Status = eProgressStatus.Waiting;
            Add(pi);
            CurrentIndex = -1;
        }
    }

    public enum eProgressStatus
    {
        Waiting = 0,
        Processing = 1,
        Done = 2,
        Canceling = 3,
        Cancelled = 4,
    }
}
