import { Forecast } from 'pages/dashboard/components/Forecast'
import { render } from '@testing-library/react'
import { Provider } from 'react-redux'
import configureMockStore, { MockStoreEnhanced } from 'redux-mock-store'
import { ForecastModel } from 'common/models/forecastModel'

describe('Forecast', () => {
  let initialState: {
    forecast: ForecastModel[]
  } = {
    forecast: []
  }
  let store: MockStoreEnhanced<any, any>

  beforeEach(() => {
    initialState = {
      forecast: []
    }
    store = configureMockStore([])({ dashboardReducer: initialState })
  })

  it('should redner empty', () => {
    const { getByText } = render(
      <Provider store={store}>
        <Forecast />
      </Provider>
    )

    expect(getByText('Repetitions forecast', { exact: false })).toBeInTheDocument()
  })

  it('should redner with data', () => {
    initialState.forecast = [
      { data: '01/01/2024', count: 1 },
      { data: '02/01/2024', count: 2 }
    ]

    const { getAllByTestId } = render(
      <Provider store={store}>
        <Forecast />
      </Provider>
    )

    expect(getAllByTestId('forecast-item-test-id').length).toBe(2)
  })
})
