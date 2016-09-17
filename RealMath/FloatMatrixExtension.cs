namespace RealMath
{
    public partial struct FloatMatrix
    {
        /// <summary>
        /// Возвращает заданную строку как вектор
        /// </summary>
        /// <param name="rowNum">Индекс строки</param>
        /// <returns></returns>
        public FloatVector GetRow(int rowNum)
        {
            int resultSize = ColumnsCount;
            float[] x = new float[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[rowNum, i];
            return new FloatVector(x);
        }

        /// <summary>
        /// Возвращает заданный столбец как вектор
        /// </summary>
        /// <param name="columnNum">Индекс столбца</param>
        /// <returns></returns>
        public FloatVector GetColumn(int columnNum)
        {
            int resultSize = RowsCount;
            float[] x = new float[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[i, columnNum];
            return new FloatVector(x);
        }
    }
}