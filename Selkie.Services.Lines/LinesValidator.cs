using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;

namespace Selkie.Services.Lines
{
    [ProjectComponent(Lifestyle.Transient)]
    public class LinesValidator : ILinesValidator
    {
        private readonly ILinesValidatorLogger m_Logger;

        public LinesValidator([NotNull] ILinesValidatorLogger logger)
        {
            m_Logger = logger;
        }

        public bool ValidateDtos(IEnumerable <LineDto> lineDtos)
        {
            IEnumerable <LineDto> array = lineDtos as LineDto[] ?? lineDtos.ToArray();
            IEnumerable <int> ids = array.Select(x => x.Id);

            bool validateDtos = ValidateIds(ids);

            m_Logger.LogLineDtos(validateDtos,
                                 array);

            return validateDtos;
        }

        public bool ValidateLines(IEnumerable <ILine> lines)
        {
            IEnumerable <ILine> array = lines as ILine[] ?? lines.ToArray();
            IEnumerable <int> ids = array.Select(x => x.Id);

            bool validateLines = ValidateIds(ids);

            m_Logger.LogLines(validateLines, array);

            return validateLines;
        }

        private bool ValidateIds([NotNull] IEnumerable <int> ids)
        {
            int[] array = ids.ToArray();

            if ( array.Length <= 1 )
            {
                m_Logger.LogIdsAreEmpty();

                return false;
            }

            bool validateIds = Validate(array);

            m_Logger.LogValidateStatus(validateIds);

            return validateIds;
        }

        private bool Validate(int[] array)
        {
            var expectedId = 0;

            bool validateIds = array.All(id => expectedId++ == id);
            return validateIds;
        }
    }
}