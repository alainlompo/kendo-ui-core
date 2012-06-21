namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    /// <summary>
    /// Creates series for the <see cref="Chart{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the chart is bound to</typeparam>
    public class ChartSeriesFactory<TModel> : IHideObjectMembers
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSeriesFactory{TModel}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ChartSeriesFactory(Chart<TModel> container)
        {

            Container = container;
        }

        /// <summary>
        /// The parent Chart
        /// </summary>
        public Chart<TModel> Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            ChartBarSeries<TModel, TValue> barSeries = new ChartBarSeries<TModel, TValue>(Container, expression);

            Container.Series.Add(barSeries);

            return new ChartBarSeriesBuilder<TModel>(barSeries);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(string memberName)
        {
            return Bar(null, memberName);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(Type memberType, string memberName)
        {
            var valueExpr = BuildMemberExpression(memberType, memberName);
            var seriesType = typeof(ChartBarSeries<,>).MakeGenericType(typeof(TModel), valueExpr.Body.Type);
            var series = (IChartBarSeries)BuildSeries(seriesType, valueExpr);

            series.Member = memberName;

            if (!series.Name.HasValue())
            {
                series.Name = memberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartBarSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines bar series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(IEnumerable data)
        {
            ChartBarSeries<TModel, object> barSeries = new ChartBarSeries<TModel, object>(Container, data);

            Container.Series.Add(barSeries);

            return new ChartBarSeriesBuilder<TModel>(barSeries);
        }

        /// <summary>
        /// Defines bound column series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = Bar(expression);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(string memberName)
        {
            return Column(null, memberName);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(Type memberType, string memberName)
        {
            var builder = Bar(memberType, memberName);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bar series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(IEnumerable data)
        {
            var builder = Bar(data);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            ChartLineSeries<TModel, TValue> LineSeries = new ChartLineSeries<TModel, TValue>(Container, expression);

            Container.Series.Add(LineSeries);

            return new ChartLineSeriesBuilder<TModel>(LineSeries);
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(string memberName)
        {
            return Line(null, memberName);
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(Type memberType, string memberName)
        {
            var valueExpr = BuildMemberExpression(memberType, memberName);
            var seriesType = typeof(ChartLineSeries<,>).MakeGenericType(typeof(TModel), valueExpr.Body.Type);
            var series = (IChartLineSeries)BuildSeries(seriesType, valueExpr);

            series.Member = memberName;

            if (!series.Name.HasValue())
            {
                series.Name = memberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartLineSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines line series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(IEnumerable data)
        {
            ChartLineSeries<TModel, object> lineSeries = new ChartLineSeries<TModel, object>(Container, data);

            Container.Series.Add(lineSeries);

            return new ChartLineSeriesBuilder<TModel>(lineSeries);
        }

        /// <summary>
        /// Defines bound vertical line series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> VerticalLine<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = Line(expression);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound vertical line series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> VerticalLine(string memberName)
        {
            var builder = Line(memberName);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound vertical line series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> VerticalLine(Type memberType, string memberName)
        {
            var builder = Line(memberType, memberName);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines vertical line series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> VerticalLine(IEnumerable data)
        {
            var builder = Line(data);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound area series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> Area<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            ChartAreaSeries<TModel, TValue> areaSeries = new ChartAreaSeries<TModel, TValue>(Container, expression);

            Container.Series.Add(areaSeries);

            return new ChartAreaSeriesBuilder<TModel>(areaSeries);
        }

        /// <summary>
        /// Defines bound area series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> Area(string memberName)
        {
            return Area(null, memberName);
        }

        /// <summary>
        /// Defines bound area series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> Area(Type memberType, string memberName)
        {
            var valueExpr = BuildMemberExpression(memberType, memberName);
            var seriesType = typeof(ChartAreaSeries<,>).MakeGenericType(typeof(TModel), valueExpr.Body.Type);
            var series = (IChartAreaSeries)BuildSeries(seriesType, valueExpr);

            series.Member = memberName;

            if (!series.Name.HasValue())
            {
                series.Name = memberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartAreaSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines area series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> Area(IEnumerable data)
        {
            ChartAreaSeries<TModel, object> areaSeries = new ChartAreaSeries<TModel, object>(Container, data);

            Container.Series.Add(areaSeries);

            return new ChartAreaSeriesBuilder<TModel>(areaSeries);
        }

        /// <summary>
        /// Defines bound vertical area series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> VerticalArea<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = Area(expression);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound vertical area series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> VerticalArea(string memberName)
        {
            var builder = Area(memberName);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound vertical area series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> VerticalArea(Type memberType, string memberName)
        {
            var builder = Area(memberType, memberName);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines vertical area series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartAreaSeriesBuilder<TModel> VerticalArea(IEnumerable data)
        {
            var builder = Area(data);
            builder.Series.Orientation = ChartSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound scatter series.
        /// </summary>
        /// <param name="xValueExpression">
        /// The expression used to extract the X value from the chart model
        /// </param>
        /// <param name="yValueExpression">
        /// The expression used to extract the Y value from the chart model
        /// </param>
        public virtual ChartScatterSeriesBuilder<TModel> Scatter<TXValue, TYValue>(Expression<Func<TModel, TXValue>> xValueExpression, Expression<Func<TModel, TYValue>> yValueExpression)
        {
            var scatterSeries = new ChartScatterSeries<TModel, TXValue, TYValue>(Container, xValueExpression, yValueExpression);

            Container.Series.Add(scatterSeries);

            return new ChartScatterSeriesBuilder<TModel>(scatterSeries);
        }

        /// <summary>
        /// Defines bound scatter series.
        /// </summary>
        /// <param name="xMemberName">
        /// The name of the X value member.
        /// </param>
        /// <param name="yMemberName">
        /// The name of the Y value member.
        /// </param>
        public virtual ChartScatterSeriesBuilder<TModel> Scatter(string xMemberName, string yMemberName)
        {
            return Scatter(null, xMemberName, yMemberName);
        }

        /// <summary>
        /// Defines bound scatter series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value members.
        /// </param>
        /// <param name="xMemberName">
        /// The name of the X value member.
        /// </param>
        /// <param name="memberNameY>
        /// The name of the Y value member.
        /// </param>
        public virtual ChartScatterSeriesBuilder<TModel> Scatter(Type memberType, string xMemberName, string yMemberName)
        {
            var expressionX = BuildMemberExpression(memberType, xMemberName);
            var expressionY = BuildMemberExpression(memberType, yMemberName);

            var seriesType = typeof(ChartScatterSeries<,,>).MakeGenericType(typeof(TModel), expressionX.Body.Type, expressionY.Body.Type);
            var series = (IChartScatterSeries) BuildSeries(seriesType, expressionX, expressionY);

            series.XMember = xMemberName;
            series.YMember = yMemberName;

            if (!series.Name.HasValue())
            {
                series.Name = xMemberName.AsTitle() + ", " + yMemberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartScatterSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines scatter series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartScatterSeriesBuilder<TModel> Scatter(IEnumerable data)
        {
            ChartScatterSeries<TModel, object, object> scatterSeries = new ChartScatterSeries<TModel, object, object>(Container, data);

            Container.Series.Add(scatterSeries);

            return new ChartScatterSeriesBuilder<TModel>(scatterSeries);
        }

        /// <summary>
        /// Defines bound scatter line series.
        /// </summary>
        /// <param name="xValueExpression">
        /// The expression used to extract the X value from the chart model
        /// </param>
        /// <param name="yValueExpression">
        /// The expression used to extract the Y value from the chart model
        /// </param>
        public virtual ChartScatterLineSeriesBuilder<TModel> ScatterLine<TXValue, TYValue>(Expression<Func<TModel, TXValue>> xValueExpression, Expression<Func<TModel, TYValue>> yValueExpression)
        {
            var scatterLineSeries = new ChartScatterLineSeries<TModel, TXValue, TYValue>(Container, xValueExpression, yValueExpression);

            Container.Series.Add(scatterLineSeries);

            return new ChartScatterLineSeriesBuilder<TModel>(scatterLineSeries);
        }

        /// <summary>
        /// Defines bound scatter line series.
        /// </summary>
        /// <param name="xMemberName">
        /// The name of the X value member.
        /// </param>
        /// <param name="yMemberName">
        /// The name of the Y value member.
        /// </param>
        public virtual ChartScatterLineSeriesBuilder<TModel> ScatterLine(string xMemberName, string yMemberName)
        {
            return ScatterLine(null, xMemberName, yMemberName);
        }

        /// <summary>
        /// Defines bound scatter line series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value members.
        /// </param>
        /// <param name="xMemberName">
        /// The name of the X value member.
        /// </param>
        /// <param name="memberNameY>
        /// The name of the Y value member.
        /// </param>
        public virtual ChartScatterLineSeriesBuilder<TModel> ScatterLine(Type memberType, string xMemberName, string yMemberName)
        {
            var expressionX = BuildMemberExpression(memberType, xMemberName);
            var expressionY = BuildMemberExpression(memberType, yMemberName);

            var seriesType = typeof(ChartScatterLineSeries<,,>).MakeGenericType(typeof(TModel), expressionX.Body.Type, expressionY.Body.Type);
            var series = (IChartScatterLineSeries)BuildSeries(seriesType, expressionX, expressionY);

            series.XMember = xMemberName;
            series.YMember = yMemberName;

            if (!series.Name.HasValue())
            {
                series.Name = xMemberName.AsTitle() + ", " + yMemberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartScatterLineSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines scatter line series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartScatterLineSeriesBuilder<TModel> ScatterLine(IEnumerable data)
        {
            ChartScatterLineSeries<TModel, object, object> scatterLineSeries = new ChartScatterLineSeries<TModel, object, object>(Container, data);

            Container.Series.Add(scatterLineSeries);

            return new ChartScatterLineSeriesBuilder<TModel>(scatterLineSeries);
        }

        /// <summary>
        /// Defines bound bubble series.
        /// </summary>
        public virtual ChartBubbleSeriesBuilder<TModel> Bubble<TXValue, TYValue, TSizeValue>(
            Expression<Func<TModel, TXValue>> xValueExpression,
            Expression<Func<TModel, TYValue>> yValueExpression,
            Expression<Func<TModel, TSizeValue>> sizeExpression,
            Expression<Func<TModel, string>> categoryExpression = null,
            Expression<Func<TModel, string>> colorExpression = null,
            Expression<Func<TModel, bool>> visibleInLegendExpression = null
            )
        {
            var bubbleSeries = new ChartBubbleSeries<TModel, TXValue, TYValue, TSizeValue>(
                Container, xValueExpression, yValueExpression, sizeExpression,
                categoryExpression, colorExpression, visibleInLegendExpression
            );

            Container.Series.Add(bubbleSeries);

            return new ChartBubbleSeriesBuilder<TModel>(bubbleSeries);
        }

        /// <summary>
        /// Defines bound bubble series.
        /// </summary>
        public virtual ChartBubbleSeriesBuilder<TModel> Bubble(
            string xMemberName,
            string yMemberName,
            string sizeMemberName,
            string categoryMemberName = null,
            string colorMemberName = null,
            string visibleInLegendMemberName = null)
        {
            return Bubble(
                null, xMemberName, yMemberName, sizeMemberName,
                categoryMemberName, colorMemberName, visibleInLegendMemberName
            );
        }

        /// <summary>
        /// Defines bound bubble series.
        /// </summary>
        public virtual ChartBubbleSeriesBuilder<TModel> Bubble(
            Type memberType,
            string xMemberName,
            string yMemberName,
            string sizeMemberName,
            string categoryMemberName = null,
            string colorMemberName = null,
            string visibleInLegendMemberName = null)
        {
            var expressionX = BuildMemberExpression(memberType, xMemberName);
            var expressionY = BuildMemberExpression(memberType, yMemberName);
            var expressionSize = BuildMemberExpression(memberType, sizeMemberName);
            var expressionCategory = BuildMemberExpression(memberType, categoryMemberName);
            var expressionColor = BuildMemberExpression(memberType, colorMemberName);
            var expressionVisibleInLegend = BuildMemberExpression(memberType, visibleInLegendMemberName);

            var seriesType = typeof(ChartBubbleSeries<,,,>).MakeGenericType(
                typeof(TModel), expressionX.Body.Type, expressionY.Body.Type, expressionSize.Body.Type
            );
            var series = (IChartBubbleSeries) BuildSeries(
                seriesType, expressionX, expressionY, expressionSize,
                expressionCategory, expressionColor, expressionVisibleInLegend
            );

            if (!series.Name.HasValue())
            {
                series.Name = xMemberName.AsTitle() + ", " + yMemberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartBubbleSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines bubble series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartBubbleSeriesBuilder<TModel> Bubble(IEnumerable data)
        {
            var bubbleSeries = new ChartBubbleSeries<TModel, object, object, object>(Container, data);

            Container.Series.Add(bubbleSeries);

            return new ChartBubbleSeriesBuilder<TModel>(bubbleSeries);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory)
        {
            ChartPieSeries<TModel, TValue> pieSeries = new ChartPieSeries<TModel, TValue>(Container, expressionValue, expressionCategory, null, null, null);

            Container.Series.Add(pieSeries);

            return new ChartPieSeriesBuilder<TModel>(pieSeries);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory, Expression<Func<TModel, string>> expressionColor, Expression<Func<TModel, bool>> expressionExplode)
        {
            ChartPieSeries<TModel, TValue> pieSeries = new ChartPieSeries<TModel, TValue>(Container, expressionValue, expressionCategory, expressionColor, expressionExplode, null);

            Container.Series.Add(pieSeries);

            return new ChartPieSeriesBuilder<TModel>(pieSeries);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory, Expression<Func<TModel, string>> expressionColor, Expression<Func<TModel, bool>> expressionExplode, Expression<Func<TModel, bool>> expressionVisibleInLegend)
        {
            ChartPieSeries<TModel, TValue> pieSeries = new ChartPieSeries<TModel, TValue>(Container, expressionValue, expressionCategory, expressionColor, expressionExplode, expressionVisibleInLegend);

            Container.Series.Add(pieSeries);

            return new ChartPieSeriesBuilder<TModel>(pieSeries);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie(string valueMemberName, string categoryMemberName)
        {
            return Pie(null, valueMemberName, categoryMemberName, "", "", "");
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="valueMemberName">
        /// The name of the value member.
        /// </param>
        /// <param name="categoryMemberName">
        /// The name of the category member.
        /// </param>
        /// <param name="explodeMemberName">
        /// The name of the explode member.
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie(string valueMemberName, string categoryMemberName, string colorMemberName, string explodeMemberName, string visibleInLegendMemberName)
        {
            return Pie(null, valueMemberName, categoryMemberName, colorMemberName, explodeMemberName, visibleInLegendMemberName);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="valueMemberName">
        /// The name of the value member.
        /// </param>
        /// <param name="categoryMemberName">
        /// The name of the category member.
        /// </param>
        /// <param name="explodeMemberName">
        /// The name of the explode member.
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie(Type memberType, string valueMemberName, string categoryMemberName, string colorMemberName, string explodeMemberName, string visibleInLegendMemberName)
        {
            var valueExpr = BuildMemberExpression(memberType, valueMemberName);
            var categoryExpr = BuildMemberExpression(typeof(string), categoryMemberName);
            var colorExpr = colorMemberName.HasValue() ? BuildMemberExpression(typeof(string), colorMemberName) : null;
            var explodeExpr = explodeMemberName.HasValue() ? BuildMemberExpression(typeof(bool), explodeMemberName) : null;
            var visibleInlegendExpr = visibleInLegendMemberName.HasValue() ? BuildMemberExpression(typeof(bool), visibleInLegendMemberName) : null;

            var seriesType = typeof(ChartPieSeries<,>).MakeGenericType(typeof(TModel), valueExpr.Body.Type);
            var series = (IChartPieSeries)BuildSeries(seriesType, valueExpr, categoryExpr, colorExpr, explodeExpr, visibleInlegendExpr);

            if (!series.Name.HasValue())
            {
                series.Name = valueMemberName.AsTitle();
            }

            if (!series.Member.HasValue())
            {
                series.Member = valueMemberName.AsTitle();
            }

            if (!series.CategoryMember.HasValue())
            {
                series.CategoryMember = categoryMemberName.AsTitle();
            }

            if (!series.ColorMember.HasValue())
            {
                series.ColorMember = colorMemberName.AsTitle();
            }

            if (!series.ExplodeMember.HasValue())
            {
                series.ExplodeMember = explodeMemberName.AsTitle();
            }

            if (!series.VisibleInLegendMember.HasValue())
            {
                series.VisibleInLegendMember = visibleInLegendMemberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartPieSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines pie series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartPieSeriesBuilder<TModel> Pie(IEnumerable data)
        {
            ChartPieSeries<TModel, object> pieSeries = new ChartPieSeries<TModel, object>(Container, data);

            Container.Series.Add(pieSeries);

            return new ChartPieSeriesBuilder<TModel>(pieSeries);
        }

        /// <summary>
        /// Defines bound donut series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory)
        {
            ChartDonutSeries<TModel, TValue> donutSeries = new ChartDonutSeries<TModel, TValue>(Container, expressionValue, expressionCategory, null, null, null);

            Container.Series.Add(donutSeries);

            return new ChartDonutSeriesBuilder<TModel>(donutSeries);
        }

        /// <summary>
        /// Defines bound donut series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory, Expression<Func<TModel, string>> expressionColor, Expression<Func<TModel, bool>> expressionExplode)
        {
            ChartDonutSeries<TModel, TValue> donutSeries = new ChartDonutSeries<TModel, TValue>(Container, expressionValue, expressionCategory, expressionColor, expressionExplode, null);

            Container.Series.Add(donutSeries);

            return new ChartDonutSeriesBuilder<TModel>(donutSeries);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="expressionValue">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut<TValue>(Expression<Func<TModel, TValue>> expressionValue, Expression<Func<TModel, string>> expressionCategory, Expression<Func<TModel, string>> expressionColor, Expression<Func<TModel, bool>> expressionExplode, Expression<Func<TModel, bool>> expressionVisibleInLegend)
        {
            ChartDonutSeries<TModel, TValue> donutSeries = new ChartDonutSeries<TModel, TValue>(Container, expressionValue, expressionCategory, expressionColor, expressionExplode, expressionVisibleInLegend);

            Container.Series.Add(donutSeries);

            return new ChartDonutSeriesBuilder<TModel>(donutSeries);
        }

        /// <summary>
        /// Defines bound donut series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut(string valueMemberName, string categoryMemberName)
        {
            return Donut(null, valueMemberName, categoryMemberName, "", "", "");
        }

        /// <summary>
        /// Defines bound donut series.
        /// </summary>
        /// <param name="valueMemberName">
        /// The name of the value member.
        /// </param>
        /// <param name="categoryMemberName">
        /// The name of the category member.
        /// </param>
        /// <param name="explodeMemberName">
        /// The name of the explode member.
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut(string valueMemberName, string categoryMemberName, string colorMemberName, string explodeMemberName, string visibleInLegendMemberName)
        {
            return Donut(null, valueMemberName, categoryMemberName, colorMemberName, explodeMemberName, visibleInLegendMemberName);
        }

        /// <summary>
        /// Defines bound pie series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="valueMemberName">
        /// The name of the value member.
        /// </param>
        /// <param name="categoryMemberName">
        /// The name of the category member.
        /// </param>
        /// <param name="explodeMemberName">
        /// The name of the explode member.
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut(Type memberType, string valueMemberName, string categoryMemberName, string colorMemberName, string explodeMemberName, string visibleInLegendMemberName)
        {
            var valueExpr = BuildMemberExpression(memberType, valueMemberName);
            var categoryExpr = BuildMemberExpression(typeof(string), categoryMemberName);
            var colorExpr = colorMemberName.HasValue() ? BuildMemberExpression(typeof(string), colorMemberName) : null;
            var explodeExpr = explodeMemberName.HasValue() ? BuildMemberExpression(typeof(bool), explodeMemberName) : null;
            var visibleInlegendExpr = visibleInLegendMemberName.HasValue() ? BuildMemberExpression(typeof(bool), visibleInLegendMemberName) : null;

            var seriesType = typeof(ChartDonutSeries<,>).MakeGenericType(typeof(TModel), valueExpr.Body.Type);
            var series = (IChartDonutSeries)BuildSeries(seriesType, valueExpr, categoryExpr, colorExpr, explodeExpr, visibleInlegendExpr);

            if (!series.Name.HasValue())
            {
                series.Name = valueMemberName.AsTitle();
            }

            if (!series.Member.HasValue())
            {
                series.Member = valueMemberName.AsTitle();
            }

            if (!series.CategoryMember.HasValue())
            {
                series.CategoryMember = categoryMemberName.AsTitle();
            }

            if (!series.ColorMember.HasValue())
            {
                series.ColorMember = colorMemberName.AsTitle();
            }

            if (!series.ExplodeMember.HasValue())
            {
                series.ExplodeMember = explodeMemberName.AsTitle();
            }

            if (!series.VisibleInLegendMember.HasValue())
            {
                series.VisibleInLegendMember = visibleInLegendMemberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartDonutSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines donut series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartDonutSeriesBuilder<TModel> Donut(IEnumerable data)
        {
            ChartDonutSeries<TModel, object> donutSeries = new ChartDonutSeries<TModel, object>(Container, data);

            Container.Series.Add(donutSeries);

            return new ChartDonutSeriesBuilder<TModel>(donutSeries);
        }

        private LambdaExpression BuildMemberExpression(Type memberType, string memberName)
        {
            const bool liftMemberAccess = false;
            var expression = ExpressionBuilder.Lambda<TModel>(memberType, memberName, liftMemberAccess);

            if (typeof(TModel).IsDynamicObject() && memberType != null && expression.Body.Type.GetNonNullableType() != memberType.GetNonNullableType())
            {
                expression = Expression.Lambda(Expression.Convert(expression.Body, memberType), expression.Parameters);
            }

            return expression;
        }

        private object BuildSeries(Type seriesType, params LambdaExpression[] expressions)
        {
            var ctorTypeArgs = new List<Type>();
            ctorTypeArgs.Add(Container.GetType());
            ctorTypeArgs.AddRange(from e in expressions select e != null ? e.GetType() : typeof(object));

            var constructor = seriesType.GetConstructor(ctorTypeArgs.ToArray()) ?? seriesType.GetConstructors()[0];

            var ctorArgs = new List<object>();
            ctorArgs.Add(Container);
            ctorArgs.AddRange(expressions);

            return constructor.Invoke(ctorArgs.ToArray());
        }
    }
}