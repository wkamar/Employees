//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;

namespace employee_apis.Models
{
    [Serializable()]
    public partial class Employee : INotifyPropertyChanging, INotifyPropertyChanged, ICloneable {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);

        private int _ID;

        private string _Code;

        private string _FirstName;

        private string _LastName;

        private bool _Gender;

        private string _JobTitle;

        private int _DepartmentID;

        private Department _Department;

        public Employee()
        {
            OnCreated();
        }

        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging("ID");
                    this._ID = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(32)]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if (this._Code != value)
                {
                    this.OnCodeChanging(value);
                    this.SendPropertyChanging("Code");
                    this._Code = value;
                    this.SendPropertyChanged("Code");
                    this.OnCodeChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(64)]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if (this._FirstName != value)
                {
                    this.OnFirstNameChanging(value);
                    this.SendPropertyChanging("FirstName");
                    this._FirstName = value;
                    this.SendPropertyChanged("FirstName");
                    this.OnFirstNameChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(64)]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if (this._LastName != value)
                {
                    this.OnLastNameChanging(value);
                    this.SendPropertyChanging("LastName");
                    this._LastName = value;
                    this.SendPropertyChanged("LastName");
                    this.OnLastNameChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.Required()]
        public virtual bool Gender
        {
            get
            {
                return this._Gender;
            }
            set
            {
                if (this._Gender != value)
                {
                    this.OnGenderChanging(value);
                    this.SendPropertyChanging("Gender");
                    this._Gender = value;
                    this.SendPropertyChanged("Gender");
                    this.OnGenderChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public virtual string JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                if (this._JobTitle != value)
                {
                    this.OnJobTitleChanging(value);
                    this.SendPropertyChanging("JobTitle");
                    this._JobTitle = value;
                    this.SendPropertyChanged("JobTitle");
                    this.OnJobTitleChanged();
                }
            }
        }

        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int DepartmentID
        {
            get
            {
                return this._DepartmentID;
            }
            set
            {
                if (this._DepartmentID != value)
                {
                    this.OnDepartmentIDChanging(value);
                    this.SendPropertyChanging("DepartmentID");
                    this._DepartmentID = value;
                    this.SendPropertyChanged("DepartmentID");
                    this.OnDepartmentIDChanged();
                }
            }
        }

        public virtual Department Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if (this._Department != value)
                {
                    this.OnDepartmentChanging(value);
                    this.SendPropertyChanging("Department");
                    this._Department = value;
                    this.SendPropertyChanged("Department");
                    this.OnDepartmentChanged();
                }
            }
        }

        #region Extensibility Method Definitions

        partial void OnCreated();
        partial void OnIDChanging(int value);

        partial void OnIDChanged();
        partial void OnCodeChanging(string value);

        partial void OnCodeChanged();
        partial void OnFirstNameChanging(string value);

        partial void OnFirstNameChanged();
        partial void OnLastNameChanging(string value);

        partial void OnLastNameChanged();
        partial void OnGenderChanging(bool value);

        partial void OnGenderChanged();
        partial void OnJobTitleChanging(string value);

        partial void OnJobTitleChanged();
        partial void OnDepartmentIDChanging(int value);

        partial void OnDepartmentIDChanged();
        partial void OnDepartmentChanging(Department value);

        partial void OnDepartmentChanged();

        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            Employee obj = new Employee();
            obj.ID = ID;
            obj.Code = Code;
            obj.FirstName = FirstName;
            obj.LastName = LastName;
            obj.Gender = Gender;
            obj.JobTitle = JobTitle;
            obj.DepartmentID = DepartmentID;
            return obj;
        }

        #endregion

        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {
            var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
