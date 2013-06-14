using System;
using Skahal.Infrastructure.Framework.Globalization;
using Skahal.Infrastructure.Framework;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Logging;
using UnityEngine;
using System.Linq;

namespace Skahal.Infrastructure.Unity.Globalization
{
	/// <summary>
	/// Globalization label repository.
	/// </summary>
	public class GlobalizationLabelRepository 
		: MemoryRepository<GlobalizationLabel, string>, IGlobalizationLabelRepository
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Globalization.GlobalizationLabelRepository"/> class.
		/// </summary>
		public GlobalizationLabelRepository() : base((e) => { return Guid.NewGuid().ToString(); })
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Skahal.Infrastructure.Unity.Globalization.GlobalizationLabelRepository"/> class.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		public GlobalizationLabelRepository (IUnitOfWork<string> unitOfWork) : base(unitOfWork, (e) => { return Guid.NewGuid().ToString(); })
		{
			Initialize ();
		}
		#endregion

		#region Private methods
		private void Initialize()
		{
			PrepareCurrentCulture();

			GlobalizationService.CultureChanged += delegate {
				PrepareCurrentCulture();
			};
		}

		private void PrepareCurrentCulture ()
		{
			var cultureName = GlobalizationService.CurrentCulture.Name;

			if (CountAll(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0) {
				LogService.Debug ("GlobalizationLabelRepository :: Loading texts for language '{0}'...", cultureName);

				var textsFilePath = string.Format ("texts.{0}", cultureName);
				var fileTextAsset = (TextAsset)UnityEngine.Resources.Load (textsFilePath) as TextAsset;
				var lines = fileTextAsset.text.Split (new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

				LogService.Debug ("GlobalizationLabelRepository :: {0} texts founds...", lines.Length);

				foreach (var line in lines) {
					var lineParts = line.Split ('=');
					Entities.Add(new GlobalizationLabel() 
					{
						EnglishText = lineParts [0].Trim (),
						CultureText = lineParts [1].Trim ().Replace(@"\n", System.Environment.NewLine),
						CultureName = cultureName
					});
				}
			}
		}
		#endregion
	}
}