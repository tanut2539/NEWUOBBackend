// Chart
var defensive = {
    labels: ['', ''],
    series: [85, 15],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#2F71C0', '#003E7E']
    },
    colors: ['#2F71C0', '#003E7E'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth10 = {
    labels: ['', '', ''],
    series: [60, 30, 10],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#109AD6', '#2B7CC5', '#003E7E']
    },
    colors: ['#109AD6', '#2B7CC5', '#003E7E'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth20 = {
    labels: ['', '', ''],
    series: [40, 40, 20],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#2F71C0', '#0EA9DC', '#003E7E']
    },
    colors: ['#2F71C0', '#0EA9DC', '#003E7E'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth40 = {
    labels: ['', '', ''],
    series: [30, 30, 40],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#2B7CC5', '#003E7E', '#0EA9DC']
    },
    colors: ['#2B7CC5', '#003E7E', '#0EA9DC'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth15 = {
    labels: ['', '', '', ''],
    series: [15, 25, 25, 35],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#F7A48C', '#2B7CC5', '#003E7E', '#0EA9DC']
    },
    colors: ['#F7A48C', '#2B7CC5', '#003E7E', '#0EA9DC'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var sustainablegrowth60 = {
    labels: ['', '', ''],
    series: [15, 25, 60],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#FF5957', '#E63633', '#FF7473']
    },
    colors: ['#FF5957', '#E63633', '#FF7473'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth60 = {
    labels: ['', '', ''],
    series: [20, 20, 60],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#2F71C0', '#004D88', '#0EA9DC']
    },
    colors: ['#2F71C0', '#004D88', '#0EA9DC'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth80 = {
    labels: ['', '', ''],
    series: [80, 10, 10],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#109AD6', '#2F71C0', '#003E7E']
    },
    colors: ['#109AD6', '#2F71C0', '#003E7E'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var growth100 = {
    labels: [''],
    series: [100],
    stroke: {
        show: true,
        curve: 'smooth',
        lineCap: 'butt',
        colors: undefined,
        width: 8,
        dashArray: 0,      
    },
    chart: {
        type: 'donut',
        height: '260px'
    },
    fill: {
        colors: ['#109AD6']
    },
    colors: ['#109AD6'],
    legend: {
        show: false
    },
    dataLabels: {
        formatter: function (val, opts) {
            return opts.w.config.series[opts.seriesIndex]
        },
    },
    responsive: [{
        breakpoint: 900,
        options: {
            chart: {
                width: 300
            },
            stroke: {
                show: true,
                curve: 'smooth',
                lineCap: 'butt',
                colors: undefined,
                width: 10,
                dashArray: 0,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var defensive_chart = new ApexCharts(document.querySelector("#defensive-chart"), defensive);
var defensive_chart_m = new ApexCharts(document.querySelector("#defensive-chart-m"), defensive);

var growth10_chart = new ApexCharts(document.querySelector("#growth-10-chart"), growth10);
var growth10_chart_m = new ApexCharts(document.querySelector("#growth-10-chart-m"), growth10);

var growth15_chart = new ApexCharts(document.querySelector("#growth-15-chart"), growth15);
var growth15_chart_m = new ApexCharts(document.querySelector("#growth-15-chart-m"), growth15);

var growth20_chart = new ApexCharts(document.querySelector("#growth-20-chart"), growth20);
var growth20_chart_m = new ApexCharts(document.querySelector("#growth-20-chart-m"), growth20);

var growth40_chart = new ApexCharts(document.querySelector("#growth-40-chart"), growth40);
var growth40_chart_m = new ApexCharts(document.querySelector("#growth-40-chart-m"), growth40);

var sustainablegrowth60_chart = new ApexCharts(document.querySelector("#sustainablegrowth-60-chart"), sustainablegrowth60);
var sustainablegrowth60_chart_m = new ApexCharts(document.querySelector("#sustainablegrowth-60-chart-m"), sustainablegrowth60);

var growth60_chart = new ApexCharts(document.querySelector("#growth-60-chart"), growth60);
var growth60_chart_m = new ApexCharts(document.querySelector("#growth-60-chart-m"), growth60);

var growth80_chart = new ApexCharts(document.querySelector("#growth-80-chart"), growth80);
var growth80_chart_m = new ApexCharts(document.querySelector("#growth-80-chart-m"), growth80);

var growth100_chart = new ApexCharts(document.querySelector("#growth-100-chart"), growth100);
var growth100_chart_m = new ApexCharts(document.querySelector("#growth-100-chart-m"), growth100);

defensive_chart.render();
defensive_chart_m.render();

growth10_chart.render();
growth10_chart_m.render();

growth15_chart.render();
growth15_chart_m.render();

growth20_chart.render();
growth20_chart_m.render();

growth40_chart.render();
growth40_chart_m.render();

sustainablegrowth60_chart.render();
sustainablegrowth60_chart_m.render();

growth60_chart.render();
growth60_chart_m.render();

growth80_chart.render();
growth80_chart_m.render();

growth100_chart.render();
growth100_chart_m.render();


// Lozad
const observer = lozad();
observer.observe();

// Swiper
var swiper1 = new Swiper(".mySwiper-1", {
    navigation: {
        nextEl: "#sw-bt-1.swiper-button-next",
        prevEl: "#sw-bt-2.swiper-button-prev",
    },
    pagination: {
        el: "#sw-pg-1.swiper-pagination",
    },
});

var swiper2 = new Swiper(".mySwiper-2", {
    navigation: {
        nextEl: "#sw-bt-3.swiper-button-next",
        prevEl: "#sw-bt-4.swiper-button-prev",
    },
    pagination: {
        el: "#sw-pg-2.swiper-pagination",
    },
});