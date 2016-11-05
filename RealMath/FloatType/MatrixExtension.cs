namespace RealMath.FloatType
{
    public partial struct Matrix
    {
        /// <summary>
        /// Возвращает заданную строку как вектор
        /// </summary>
        /// <param name="rowNum">Индекс строки</param>
        /// <returns></returns>
        public Vector GetRow(int rowNum)
        {
            int resultSize = ColumnsCount;
            float[] x = new float[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[rowNum, i];
            return new Vector(x);
        }

        /// <summary>
        /// Возвращает заданный столбец как вектор
        /// </summary>
        /// <param name="columnNum">Индекс столбца</param>
        /// <returns></returns>
        public Vector GetColumn(int columnNum)
        {
            int resultSize = RowsCount;
            float[] x = new float[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[i, columnNum];
            return new Vector(x);
        }
    }
}