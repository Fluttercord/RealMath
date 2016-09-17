namespace RealMath
{
    public partial struct DoubleMatrix
    {
        /// <summary>
        /// Возвращает заданную строку как вектор
        /// </summary>
        /// <param name="rowNum">Индекс строки</param>
        /// <returns></returns>
        public DoubleVector GetRow(int rowNum)
        {
            int resultSize = ColumnsCount;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[rowNum, i];
            return new DoubleVector(x);
        }

        /// <summary>
        /// Возвращает заданный столбец как вектор
        /// </summary>
        /// <param name="columnNum">Индекс столбца</param>
        /// <returns></returns>
        public DoubleVector GetColumn(int columnNum)
        {
            int resultSize = RowsCount;
            double[] x = new double[resultSize];
            for (int i = 0; i < resultSize; i++)
                x[i] = _elements[i, columnNum];
            return new DoubleVector(x);
        }
    }
}