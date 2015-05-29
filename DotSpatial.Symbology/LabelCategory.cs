// ********************************************************************************************************
// Product Name: DotSpatial.Symbology.dll
// Description:  Contains the business logic for symbology layers and symbol categories.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Original Code is from MapWindow.dll version 6.0
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 4/21/2009 1:29:09 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//
// ********************************************************************************************************

using System.Drawing;
using DotSpatial.Serialization;
using System.Data;

namespace DotSpatial.Symbology
{
    public class LabelCategory : ILabelCategory
    {
        #region Private Variables

        [Serialize("Expression")]
        private string _expression;

        [Serialize("FilterExpression")]
        private string _filterExpression;

        [Serialize("Name")]
        private string _name;

        [Serialize("SelectionSymbolizer")]
        private ILabelSymbolizer _selectionSymbolizer;

        [Serialize("Symbolizer")]
        private ILabelSymbolizer _symbolizer;


        private Expression _exp;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of LabelCategory
        /// </summary>
        public LabelCategory()
        {
            _symbolizer = new LabelSymbolizer();
            _selectionSymbolizer = new LabelSymbolizer { FontColor = Color.Cyan };
            _exp = new Expression();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the Copy() method cast as an object.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return Copy();
        }

        ILabelCategory ILabelCategory.Copy()
        {
            return Copy();
        }

        /// <summary>
        /// Returns a shallow copy of this category with the exception of
        /// the TextSymbolizer, which is duplicated.  This uses memberwise
        /// clone, so sublcasses using this method will return an appropriate
        /// version.
        /// </summary>
        /// <returns>A shallow copy of this object.</returns>
        public virtual LabelCategory Copy()
        {
            var result = MemberwiseClone() as LabelCategory;
            if (result == null) return null;
            result.Symbolizer = Symbolizer.Copy();
            result.SelectionSymbolizer = SelectionSymbolizer.Copy();
            return result;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "<No Name>" : Name;
        }

        /// <summary>
        /// Updates the Expression-Object with the columns that exist inside the features that belong to this category. They are used for calculating the expression.
        /// </summary>
        /// <param name="columns">Columns that should be updated.</param>
        /// <returns>False if columns were not set.</returns>
        public bool UpdateExpressionColumns(DataColumnCollection columns)
        {
            return _exp.UpdateFields(columns);
        }

        /// <summary>
        /// Calculates the expression for the given row. The Expression.Columns have to be in sync with the Features columns for calculation to work without error.
        /// </summary>
        /// <param name="row">Datarow the expression gets calculated for.</param>
        /// <param name="selected">Indicates whether the feature is selected.</param>
        /// <param name="FID">The FID of the feature, the expression gets calculated for.</param>
        /// <returns>null if there was an error while parsing the expression, else the calculated expression</returns>
        public string CalculateExpression(DataRow row, bool selected, int FID)
        {
            string ff = (selected ? _selectionSymbolizer : _symbolizer).FloatingFormat;
            _exp.FloatingFormat = ff != null ? ff.Trim() : "";
            _exp.ParseExpression(_expression);
            return _exp.CalculateRowValue(row, FID);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the string filter expression that controls which features
        /// that this should apply itself to.
        /// </summary>
        public string FilterExpression
        {
            get { return _filterExpression; }
            set { _filterExpression = value; }
        }

        /// <summary>
        /// Gets or sets the string expression that controls the integration of field values into the label text. 
        /// This is the raw text that is used to do calculations and concat fields and strings.
        /// </summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        /// <summary>
        /// Gets or sets the string name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the text symbolizer to use for this category
        /// </summary>
        public ILabelSymbolizer Symbolizer
        {
            get { return _symbolizer; }
            set { _symbolizer = value; }
        }

        /// <summary>
        /// Gets or sets the text symbolizer to use for this category
        /// </summary>
        public ILabelSymbolizer SelectionSymbolizer
        {
            get { return _selectionSymbolizer; }
            set { _selectionSymbolizer = value; }
        }

        #endregion
    }
}